﻿using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Polly;
using Polly.Retry;
using PropelAuth.Api.Extensions;
using PropelAuth.Api.Models;

namespace PropelAuth.Api
{
    public partial class PropelAuthApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="PropelAuthApiClient"/> class.  This assumes that the HttpClient is configured correctly with a base URI address and a corresponding Bearer token.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public PropelAuthApiClient(HttpClient httpClient) {
            _httpClient = httpClient;
            _retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
        #endregion

        #region Http Methods

        private HttpResponseMessage CheckStatusCode(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode) {
                var responseMessage = response.StatusCode switch {
                    System.Net.HttpStatusCode.BadRequest => "Bad Request.",
                    System.Net.HttpStatusCode.Unauthorized => "API Key is incorrect",
                    System.Net.HttpStatusCode.NotFound => "API not found.",
                    System.Net.HttpStatusCode.TooManyRequests => "Too Many Requests.  Please wait and try again shortly.",
                    System.Net.HttpStatusCode.UpgradeRequired => "Cannot use organizations unless B2B support is enabled--enable it in your PropelAuth dashboard",
                    _ => $"Unknown error when performing operation.  Status code: {response.StatusCode}.",
                };
                throw new HttpRequestException(responseMessage, null, response.StatusCode);
            }
            return response;
        }


        /// <summary>
        /// Execute a Http GET request to the specified relative endpoint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        private async Task<T?> GetAsync<T>(string endpoint) {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(endpoint));
            CheckStatusCode(response);
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
                return default;
            return JsonSerializer.Deserialize<T>(content);
        }

        /// <summary>
        /// Execute a Http GET request to the specified relative endpoint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        private async Task<string?> GetJsonAsync(string endpoint) {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(endpoint));
            CheckStatusCode(response);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Execute a Http POST request to the specified relative endpoint with the specified payload.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload) {
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync(endpoint, content));
            CheckStatusCode(response);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseContent);
        }

        /// <summary>
        /// Execute a Http POST request to the specified relative endpoint which returns the specified response type.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        private async Task<TResponse?> PostWithResponseAsync<TResponse>(string endpoint) {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync(endpoint, null));
            CheckStatusCode(response);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseContent);
        }

        /// <summary>
        /// Execute a Http POST request to the specified relative endpoint with an empty json payload and no response needed.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        private async Task PostEmptyPayloadAsync(string endpoint) {
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync(endpoint, content));
            CheckStatusCode(response);
        }

        /// <summary>
        /// Execute a Http POST request to the specified relative endpoint with the specified payload.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private async Task<TResponse?> PostEmptyPayloadAsync<TResponse>(string endpoint) {
            var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync(endpoint, content));
            CheckStatusCode(response);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseContent);
        }

        /// <summary>
        /// Execute a Http POST request to the specified relative endpoint with the specified payload.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="payload">The payload.</param>
        private async Task PostEmptyResponseAsync<TRequest>(string endpoint, TRequest payload) {
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync(endpoint, content));
            CheckStatusCode(response);
        }

        /// <summary>
        /// Execute a Http PUT request to the specified relative endpoint with the specified payload.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="payload">The payload.</param>
        private async Task PutAsync<TRequest>(string endpoint, TRequest payload) {
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PutAsync(endpoint, content));
            CheckStatusCode(response);
        }

        /// <summary>
        /// Execute a Http DELETE request to the specified relative endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        private async Task DeleteAsync(string endpoint) {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.DeleteAsync(endpoint));
            CheckStatusCode(response);
        }

        /// <summary>
        /// Execute a Http DELETE request to the specified relative endpoint.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        private async Task<TResponse?> DeleteAsync<TResponse>(string endpoint) {
            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.DeleteAsync(endpoint));
            CheckStatusCode(response);
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
                return default;
            return JsonSerializer.Deserialize<TResponse>(content);
        }

        /// <summary>
        /// Execute a Http DELETE request to the specified relative endpoint with the corresponding payload.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private async Task DeleteWithPayloadAsync<TRequest>(string endpoint, TRequest payload) {
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint) {
                Content = content
            };

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.SendAsync(request));
            CheckStatusCode(response);
        }
        #endregion

        #region User APIs

        /// <summary>
        /// Create a new user and return the newly created user id.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>If successful, the newly created user id.</returns>
        public async Task<string?> CreateUserAsync(CreateUserRequest request) {
            return await PostAsync<CreateUserRequest, string>("/api/backend/v1/user/", request);
        }

        /// <summary>
        /// Fetches the user by their user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="includeOrgs">if set to <c>true</c> [include orgs].</param>
        /// <returns>The <see cref="PropelAuthUser"/> instace that matches or null.</returns>
        public async Task<PropelAuthUser?> FetchUserByIdAsync(string userId, bool includeOrgs = false) {
            return await GetAsync<PropelAuthUser?>($"/api/backend/v1/user/{userId}?include_orgs={includeOrgs.ToString().ToLower()}");
        }

        /// <summary>
        /// Fetches the user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <param name="includeOrgs">if set to <c>true</c> [include orgs].</param>
        /// <returns>The <see cref="PropelAuthUser"/> instace that matches or null.</returns>
        public async Task<PropelAuthUser?> FetchUserByEmailAsync(string email, bool includeOrgs = false) {
            return await GetAsync<PropelAuthUser?>($"/api/backend/v1/user/email?email={email.UrlEncode()}&include_orgs={includeOrgs.ToString().ToLower()}");
        }

        /// <summary>
        /// Fetches the user by username (if you have usernames enabled).
        /// </summary>
        /// <param name="userName">Username of the user.</param>
        /// <param name="includeOrgs">if set to <c>true</c> inlude any Organizations.</param>
        /// <returns>The <see cref="PropelAuthUser"/> instace that matches or null.</returns>
        public async Task<PropelAuthUser?> FetchUserByUsernameAsync(string userName, bool includeOrgs = false) {
            return await GetAsync<PropelAuthUser?>($"/api/backend/v1/user/username?username={userName.UrlEncode()}&include_orgs={includeOrgs.ToString().ToLower()}");
        }

        /// <summary>
        /// Paginates through the list of users. You can also filter the list by a partial email/username match, and sort the list by different fields.
        /// </summary>
        /// <param name="emailOrUsername">A partial email or username to filter the list by.</param>
        /// <param name="legacyUserId">The user id from another system, such as a former auth provider. We'll return any users that contain this id.</param>
        /// <param name="orderBy">How to order any returned records.</param>
        /// <param name="includeOrgs">Whether to include the user's orgs in the response.</param>
        /// <param name="pageNumber">The page number to return.  Default is 0</param>
        /// <param name="pageSize">The number of users to return per page.</param>
        /// <returns></returns>
        public async Task<FindUserResponse?> FindUsersAsync(FindUserRequest request) {
            var ordering = request.OrderBy switch {
                FindUserOrderBy.CreatedAtDesc => "CREATED_AT_DESC",
                FindUserOrderBy.CreatedAtAsc => "CREATED_AD_ASC",
                FindUserOrderBy.LastActiveAtAsc => "LAST_ACTIVE_AT_ASC",
                FindUserOrderBy.LastActiveAtDesc => "LAST_ACTIVE_AT_DESC",
                FindUserOrderBy.Username => "USERNAME",
                _ => "EMAIL",
            };
            return await GetAsync<FindUserResponse?>($"/api/backend/v1/user/query?email_or_username={request.EmailOrUsername?.UrlEncode()}&legacy_user_id={request.LegacyUserId?.UrlEncode()}&include_orgs={request.IncludeOrgs.ToString().ToLower()}&order_by={ordering}&page_number={request.PageNumber}&page_size={request.PageSize}");
        }

        /// <summary>
        /// Update various properties for a user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="request">The <see cref="UpdateUserRequest"> request.</param>
        public async Task<bool> UpdateUserAsync(string userId, UpdateUserRequest request) {
            return await PostAsync<UpdateUserRequest, bool>($"/api/backend/v1/user/${userId}", request);
        }

        /// <summary>
        /// Updates a user's email address. You can choose whether to update it immediately or send an email to confirm the new email address.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="request">The <see cref="UpdateEmailRequest"> request.</param>
        public async Task UpdateEmailAsync(string userId, UpdateEmailRequest request) {
            await PostEmptyResponseAsync($"/api/backend/v1/user/${userId}/email", request);
        }

        /// <summary>
        /// Updates the password asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="request">The request.</param>
        public async Task<bool> UpdatePasswordAsync(string userId, UpdatePasswordRequest request) {
            return await PostAsync<UpdatePasswordRequest, bool>($"/api/backend/v1/user/{userId}/password", request);
        }

        /// <summary>
        /// Creates a magic link that a user can use to log in. You can customize the expiration and the location the user is redirected to after logging in. 
        /// Note that this doesn't send the email to the user, it just generates it and returns the URL.
        /// </summary>
        /// <param name="request">The <see cref="CreateMagicLinkRequest"> request.</param>
        /// <returns>The <see cref="CreateMagicLinkResponse"/> response.</returns>
        public async Task<CreateMagicLinkResponse?> CreateMagicLinkAsync(CreateMagicLinkRequest request) {
            return await PostAsync<CreateMagicLinkRequest, CreateMagicLinkResponse>($"/api/backend/v1/magic_link", request);
        }

        /// <summary>
        /// Generates an access token for a user. This can be used to test your backend without a frontend in tools like cURL or Postman. 
        /// You can also choose the duration, which is useful for generating long-lived tokens for testing.
        /// </summary>
        /// <param name="request">The <see cref="CreateAccessTokenRequest"> request.</param>
        /// <returns>The <see cref="CreateAccessTokenResponse"/> response.</returns>
        public async Task<CreateAccessTokenResponse?> CreateAccessTokenAsync(CreateAccessTokenRequest request) {
            return await PostAsync<CreateAccessTokenRequest, CreateAccessTokenResponse>($"/api/backend/v1/access_token", request);
        }

        /// <summary>
        /// Migrates a user from an external source. This is similar to the Create User API, but for cases where the user already exists in another system. 
        /// With this API you can maintain the user's existing ID, password (via a hashed password), and 2FA.
        /// </summary>
        /// <param name="request">The <see cref="MigrateUserRequest"> request.</param>
        /// <returns>The <see cref="MigrateUserResponse"/> response.</returns>
        public async Task<MigrateUserResponse?> MigrateUserAsync(MigrateUserRequest request) {
            return await PostAsync<MigrateUserRequest, MigrateUserResponse>($"/api/backend/v1/migrate_user/", request);
        }

        /// <summary>
        /// Deletes a user. This will permanently delete the user and all of the user's data.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        public async Task<bool> DeleteUserAsync(string userId) {
            return await DeleteAsync<bool>($"/api/backend/v1/user/{userId}");
        }

        /// <summary>
        /// Disables / blocks a user. This will prevent the user from logging in until the user is enabled again.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        public async Task<bool> DisableUserAsync(string userId) {
            return await PostWithResponseAsync<bool>($"/api/backend/v1/user/{userId}/disable");
        }

        /// <summary>
        /// Enables / Unblocks a user that was previously disabled / blocked.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        public async Task<bool> EnableUserAsync(string userId) {
            return await PostWithResponseAsync<bool>($"/api/backend/v1/user/{userId}/enable");
        }

        /// <summary>
        /// Disables two-factor authentication for a user.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        public async Task<bool> Disable2faAsync(string userId) {
            return await PostWithResponseAsync<bool>($"/api/backend/v1/user/{userId}/disable_2fa");
        }

        /// <summary>
        /// Resend a confirmation email to an unconfirmed user.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        public async Task ResendEmailConfirmationAsync(string userId) {
            var request = new ResendEmailConfirmationRequest() {
                UserId = userId
            };
            await PostEmptyResponseAsync($"/api/backend/v1/resend_email_confirmation", request);
        }

        /// <summary>
        /// Force logout a user from all sessions.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        public async Task LogoutAllSessionsAsync(string userId) {
            await PostEmptyPayloadAsync($"/api/backend/v1/user/{userId}/logout_all_sessions");
        }

        #endregion

        #region Organization APIs

        /// <summary>
        /// Fetches an organization by ID.
        /// </summary>
        /// <param name="orgId">The organization's ID.</param>
        /// <returns></returns>
        public async Task<PropelAuthOrg?> FetchOrgByIdAsync(string orgId) {
            return await GetAsync<PropelAuthOrg?>($"/api/backend/v1/org/{orgId}");
        }

        /// <summary>
        /// Fetches organizations with optional filtering.
        /// </summary>
        /// <param name="request">The <see cref="FindOrgRequest"/> request.</param>
        /// <returns>The <see cref="FindOrgResponse"/> response.</returns>
        public async Task<FindOrgResponse?> FindOrgsAsync(FindOrgRequest request) {
            return await PostAsync<FindOrgRequest, FindOrgResponse?>($"/api/backend/v1/org/query", request);
        }

        /// <summary>
        /// Fetches users within an organization.
        /// </summary>
        /// <param name="request">The <see cref="FindUsersInOrgRequest"/> request.</param>
        /// <returns>The <see cref="FindUsersInOrgResponse"/> response.</returns>
        public async Task<FindUsersInOrgResponse?> FindUsersInOrgAsync(FindUsersInOrgRequest request) {
            return await GetAsync<FindUsersInOrgResponse?>($"/api/backend/v1/user/org/{request.OrgId}?include_orgs={request.IncludeOrgs.ToString().ToLower()}&role={request.Role.UrlEncode()}&page_size={request.PageSize}&page_number={request.PageNumber}");
        }

        /// <summary>
        /// Creates a new organization. Your users can use our hosted pages to create new organizations, 
        /// but you can also use this API to create organizations programmatically.
        /// </summary>
        /// <param name="request">The <see cref="CreateOrgRequest"/> request.</param>
        /// <returns>The <see cref="CreateOrgResponse"/> response.</returns>
        public async Task<CreateOrgResponse?> CreateOrgAsync(CreateOrgRequest request) {
            return await PostAsync<CreateOrgRequest, CreateOrgResponse>("/api/v1/backend/v1/org/", request);
        }

        /// <summary>
        /// Adds a user to an organization with the specified role. Unlike the org invitation API, this API 
        /// does not send an invitation email to the user. The user will in the organization immediately.
        /// </summary>
        /// <param name="request">The <see cref="AddUserToOrgRequest"> request.</param>
        public async Task AddUserToOrgAsync(AddUserToOrgRequest request) {
            await PostEmptyResponseAsync("/api/backend/v1/org/add_user", request);
        }

        /// <summary>
        /// Sends an invitation to a user to join an organization with the specified role. 
        /// The user will receive an email with a link to accept the invitation. 
        /// The user will not be added to the organization until they accept the invitation.
        /// </summary>
        /// <param name="request">The <see cref="InviteUserToOrgRequest"/> request.</param>
        public async Task InviteUserToOrgAsync(InviteUserToOrgRequest request) {
            await PostEmptyResponseAsync("/api/backend/v1/org/invite_user", request);
        }

        /// <summary>
        /// Change a user's role in an organization. The user must already be a member of the organization.
        /// </summary>
        /// <param name="request">The <see cref="ChangeUserOrgRoleRequest"/> request.</param>
        public async Task ChangeUserOrgRoleAsync(ChangeUserOrgRoleRequest request) {
            await PostEmptyResponseAsync("/api/backend/v1/org/change_role", request);
        }

        /// <summary>
        /// Remove a user from an organization.
        /// </summary>
        /// <param name="request">The <see cref="RemoveUserFromOrgRequest"/> request.</param>
        public async Task RemoveUserFromOrgAsync(RemoveUserFromOrgRequest request) {
            await PostEmptyResponseAsync("/api/backend/v1/org/remove_user", request);
        }

        /// <summary>
        /// Updates an organization.
        /// </summary>
        /// <param name="orgId">The organization ID.</param>
        /// <param name="request">The <see cref="UpdateOrgRequest"/> request.</param>
        public async Task UpdateOrgAsync(string orgId, UpdateOrgRequest request) {
            await PostEmptyResponseAsync($"/api/backend/v1/org/{orgId}", request);
        }

        /// <summary>
        /// Deletes an organization.
        /// </summary>
        /// <param name="orgId">The organization ID.</param>
        public async Task DeleteOrgAsync(string orgId) {
            await DeleteAsync($"/api/backend/v1/org/{orgId}");
        }

        /// <summary>
        /// Allows an organization to setup SAML SSO. Users in the organization will then be able to go through the SAML setup flow.
        /// </summary>
        /// <param name="orgId">The organization ID.</param>
        public async Task EnableSamlForOrgAsync(string orgId) {
            await PostEmptyPayloadAsync($"/api/backend/v1/org/{orgId}/allow_saml");
        }

        /// <summary>
        /// Disallows an organization to setup SAML SSO. If the organization already has SAML setup, they will no longer be able to use it.
        /// </summary>
        /// <param name="orgId">The organization ID.</param>
        public async Task DisableSamlForOrgAsync(string orgId) {
            await PostEmptyPayloadAsync($"/api/backend/v1/org/{orgId}/disallow_saml");
        }

        /// <summary>
        /// Fetches roles and permissions mappings.
        /// </summary>
        /// <returns>The <see cref="CustomRoleMappingResponse"/> response.</returns>
        public async Task<CustomRoleMappingResponse?> FetchCustomRoleMappingsAsync() {
            return await PostEmptyPayloadAsync<CustomRoleMappingResponse>("/api/backend/v1/custom_role_mappings");
        }

        /// <summary>
        /// Subscribes an organization to a custom Role Mapping.
        /// </summary>
        /// <param name="orgId">The organization ID.</param>
        /// <param name="request">The <see cref="SubscribeOrgToCustomRoleRequest"/> request.</param>
        public async Task SubscribeOrgToCustomRoleAsync(string orgId, SubscribeOrgToCustomRoleRequest request) {
            await PutAsync($"/api/backend/v1/org/{orgId}", request);
        }

        /// <summary>
        /// Fetches pending invites for orgs with optional filtering.
        /// </summary>
        /// <param name="orgId">The organization ID.</param>
        /// <param name="request">The <see cref="PendingOrgInviteRequest"/> request.</param>
        /// <returns>The <see cref="PendingOrgInviteResponse"/> response.</returns>
        public async Task<PendingOrgInviteResponse?> FetchPendingOrgInvitesAsync(string orgId, PendingOrgInviteRequest request) {
            return await GetAsync<PendingOrgInviteResponse>($"/api/backend/v1/pending_org_invites?org_id={orgId}&page_size={request.PageSize}&page_number={request.PageNumber}");
        }

        /// <summary>
        /// Deletes a user invite to an organization.
        /// </summary>
        /// <param name="request">The <see cref="RevokePendingOrgInviteRequest"/> request.</param>
        public async Task RevokePendingOrgInviteAsync(RevokePendingOrgInviteRequest request) {
            await DeleteWithPayloadAsync("/api/backend/v1/pending_org_invites", request);
        }

        /// <summary>
        /// Creates a link that allows a user to setup SAML for an organization without logging in or creating an account. 
        /// Visit our SAML/Enterprise SSO docs for more information.
        /// </summary>
        /// <param name="orgId">The organization ID.</param>
        /// <param name="request">The <see cref="CreateSamlConnectionLinkRequest"/> request.</param>
        /// <returns>The <see cref="CreateSamlConnectionLinkResponse"/> response.</returns>
        public async Task<CreateSamlConnectionLinkResponse?> CreateSamlConnectionLinkAsync(string orgId, CreateSamlConnectionLinkRequest request) {
            return await PostAsync<CreateSamlConnectionLinkRequest, CreateSamlConnectionLinkResponse>($"/api/backend/v1/org/{orgId}/create_saml_connection_link", request);
        }

        /// <summary>
        /// Fetches the SAML Service Provider Metadata. This is the information your organizations will input into their IdP when configuring SAML.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        /// <returns>The <see cref="SamlServiceProviderMetadata"/> response.</returns>
        public async Task<SamlServiceProviderMetadata?> GetSamlServiceProviderMetadataAsync(string orgId) {
            return await GetAsync<SamlServiceProviderMetadata>($"/api/backend/v1/saml_sp_metadata/{orgId}");
        }

        /// <summary>
        /// Sets the SAML metadata from an organization's IdP. Must be completed before using the SAML Go Live endpoint.
        /// </summary>
        /// <param name="request">The <see cref="SamlIdentityProviderMetadataRequest"/> request.</param>
        public async Task UpdateSamlIdentityProviderMetadataAsync(SamlIdentityProviderMetadataRequest request) {
            await PostEmptyResponseAsync("/api/backend/v1/saml_idp_metadata", request);
        }

        /// <summary>
        /// Sets an organization's SAML status to Live after using the Set SAML IdP Metadata endpoint.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        public async Task EnableSamlConnectionForOrgAsync(string orgId) {
            await PostEmptyPayloadAsync($"/api/backend/v1/saml_idp_metadata/go_live/{orgId}");
        }

        /// <summary>
        /// Deletes an organization's SAML connection.
        /// </summary>
        /// <param name="orgId">The org identifier.</param>
        public async Task DeleteSamlConnectionForOrgAsync(string orgId) {
            await DeleteAsync($"/api/backend/v1/saml_idp_metadata/{orgId}");
        }

        #endregion

        #region API Keys

        /// <summary>
        /// Validates an API key and returns any associated user or organization. This also returns any metadata attached to the API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>The <see cref="ValidateApiKeyResponse"/> response.</returns>
        public async Task<ValidateApiKeyResponse?> ValidateApiKeyAsync(string apiKey) {
            var request = new ValidateApiKeyRequest {
                ApiKey = apiKey
            };
            return await PostAsync<ValidateApiKeyRequest, ValidateApiKeyResponse>("/api/backend/v1/end_user_api_keys/validate", request);
        }

        /// <summary>
        /// Creates a new API key. This API key can be associated with a user, an organization, or no one. 
        /// If it's associated with a user, the user's information will be returned on validation. 
        /// It will also invalidated if the user is blocked/deleted. If it's associated with an organization, 
        /// the organization's information will be returned on validation. It will also invalidated if the organization is deleted. 
        /// You can associate it with both a user and an organization, in which case both will be returned on validation, 
        /// including the role and permissions of the user within the organization. Finally, you can associate it with no one, 
        /// in which case it will only be returned on validation if the API key is valid. In this case, the API key will not be invalidated 
        /// if the user or organization is deleted. For any API key, you can attach metadata, which will be returned on validation as well.
        /// </summary>
        /// <param name="request">The <see cref="CreateApiKeyRequest"/> request.</param>
        /// <returns>The <see cref="CreateApiKeyResponse"/> response.</returns>
        public async Task<CreateApiKeyResponse?> CreateApiKeyAsync(CreateApiKeyRequest request) {
            return await PostAsync<CreateApiKeyRequest, CreateApiKeyResponse>("/api/backend/v1/end_user_api_keys", request);
        }

        /// <summary>
        /// Updates an API Key. You can update the metadata or expiration, but you cannot change ownership.
        /// </summary>
        /// <param name="apiKeyId">The ID of the API key to update. This is NOT the full API key, just the ID.</param>
        /// <param name="request">The <see cref="UpdateApiKeyRequest"/> request.</param>
        public async Task UpdateApiKeyAsync(string apiKeyId, UpdateApiKeyRequest request) {
            await PostEmptyResponseAsync("/api/backend/v1/end_user_api_keys/{apiKeyId}", request);
        }

        /// <summary>
        /// Archives an API Key. You probably know this already, but after deleting/archiving an API Key, it will no longer be valid.
        /// </summary>
        /// <param name="apiKeyId">The ID of the API key to update. This is NOT the full API key, just the ID.</param>
        public async Task DeleteApiKeyAsync(string apiKeyId) {
            await DeleteAsync($"/api/backend/v1/end_user_api_keys/{apiKeyId}");
        }

        /// <summary>
        /// Fetches an API Key by it's ID. This will not return the full API key, just the metadata, expiration, and ownership.
        /// </summary>
        /// <param name="apiKeyId">The API Key identifier.</param>
        /// <returns>The corresponding <see cref="PropelAuthApiKey"/>.</returns>
        public async Task<PropelAuthApiKey?> FetchApiKeyByIdAsync(string apiKeyId) {
            return await GetAsync<PropelAuthApiKey>($"/api/backend/v1/end_user_api_keys/{apiKeyId}");
        }

        /// <summary>
        /// Fetches a list of active API Keys. This will not return the full API key, just the metadata, expiration, and ownership.
        /// </summary>
        /// <param name="request">The <see cref="FindApiKeyRequest"/> request.</param>
        /// <returns>The <see cref="FindApiKeyResponse"/> response.</returns>
        public async Task<FindApiKeyResponse?> FindActiveApiKeysAsync(FindApiKeyRequest request) {
            return await GetAsync<FindApiKeyResponse>($"/api/backend/v1/end_user_api_keys?user_id={request.UserId}&user_email={request.UserEmail?.UrlEncode()}&org_id={request.OrgId}&page_size={request.PageSize}&page_number={request.PageNumber}");
        }

        /// <summary>
        /// Fetches a list of expired API Keys. This will not return the full API key, just the metadata, expiration, and ownership.
        /// </summary>
        /// <param name="request">The <see cref="FindApiKeyRequest"/> request.</param>
        /// <returns>The <see cref="FindApiKeyResponse"/> response.</returns>
        public async Task<FindApiKeyResponse?> FindExpiredApiKeysAsync(FindApiKeyRequest request) {
            return await GetAsync<FindApiKeyResponse>($"/api/backend/v1/end_user_api_keys/archived?user_id={request.UserId}&user_email={request.UserEmail?.UrlEncode()}&org_id={request.OrgId}&page_size={request.PageSize}&page_number={request.PageNumber}");
        }

        #endregion

        #region OAuth Apis



        #endregion
    }

    public class FindApiKeyRequest 
    {
        /// <summary>
        /// Filter by the user ID of the API key owner.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Filter by the email of the API key owner.
        /// </summary>
        [JsonPropertyName("user_email")]
        public string? UserEmail { get; set; }

        /// <summary>
        /// Filter by the organization ID of the API key owner.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string? OrgId { get; set; }

        /// <summary>
        /// The page number to return.
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 0;

        /// <summary>
        /// The number of results to return per page.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;
    }

    public class FindApiKeyResponse {
        [JsonPropertyName("api_keys")]
        public List<PropelAuthApiKey> ApiKeys { get; set; } = [];

        [JsonPropertyName("total_api_keys")]
        public int TotalApiKeys { get; set; }

        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("has_more_results")]
        public bool HasMoreResults { get; set; }
    }

    public class PropelAuthApiKey 
    {
        /// <summary>
        /// The API Key internal ID.  This is not the API Key Token.
        /// </summary>
        [JsonPropertyName("api_key_id")]
        public string ApiKeyId { get; set; } = null!;

        /// <summary>
        /// If specified, this is a API Key associated with the specified User.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// If specified, this is a Organization API Key.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string? OrgId { get; set; }

        /// <summary>
        /// Timestamp that represents when the API Key was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// A <see cref="DateTime"> representation of the CreatedAt property.
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAtDate => DateTime.UnixEpoch.AddSeconds(CreatedAt);

        /// <summary>
        /// Timesamp that represents when the API Key will expire.
        /// </summary>
        [JsonPropertyName("expires_at_seconds")]
        public long? ExpiresAtSeconds { get; set; }

        /// <summary>
        /// A <see cref="DateTime"> representation of the ExpiresAtSeconds property.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAtSecondsDate => ExpiresAtSeconds.HasValue ? DateTime.UnixEpoch.AddSeconds(ExpiresAtSeconds.Value) : null;

        /// <summary>
        /// A list of Metadata to associate with the API Key.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];
    }

    public class UpdateApiKeyRequest
    {
        /// <summary>
        /// A unix timestamp of when the API key should expire. If not provided, the API key will not expire.
        /// </summary>
        [JsonPropertyName("expires_at_seconds")]
        protected long? ExpiresAtSeconds => ExpiresAt.HasValue ? (long)ExpiresAt.Value.Subtract(DateTime.UnixEpoch).TotalSeconds : null;

        /// <summary>
        /// A <see cref="DateTime"/> instance of when the API key should expire.  If not provided, the API key will not expire.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Metadata to attach to the API key. This will be returned on validation.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];
    }

    public class CreateApiKeyResponse
    {
        public required string ApiKeyId { get; set; }
        public required string ApiKeyToken { get; set; }
    }

    public class CreateApiKeyRequest
    {
        /// <summary>
        /// The ID of the organization to associate the API key with. If not provided, the API key will not be associated with an organization.
        /// </summary>
        [JsonPropertyName("org_id")]
        public string? OrgId { get; set; }

        /// <summary>
        /// The ID of the user to associate the API key with. If not provided, the API key will not be associated with a user.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// A unix timestamp of when the API key should expire. If not provided, the API key will not expire.
        /// </summary>
        [JsonPropertyName("expires_at_seconds")]
        protected long? ExpiresAtSeconds {
            get {
                if (!ExpiresAt.HasValue)
                    return null;

                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return (long)ExpiresAt.Value.Subtract(unixEpoch).TotalSeconds;
            }
        }

        /// <summary>
        /// A <see cref="DateTime"/> instance of when the API key should expire.  If not provided, the API key will not expire.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Metadata to attach to the API key. This will be returned on validation.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];
    }

    public class ValidateApiKeyRequest 
    {
        [JsonPropertyName("api_key_token")]
        public required string ApiKey { get; set; }
    }

    public class ValidateApiKeyResponse 
    {
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = [];

        [JsonPropertyName("user")]
        public PropelAuthUser? User { get; set; }

        [JsonPropertyName("org")]
        public PropelAuthOrg? Org { get; set; }

        [JsonPropertyName("user_in_org")]
        public TokenOrgMemberInfo? UserInOrg { get; set; } 
    }

    public class TokenOrgMemberInfo 
    {
        [JsonPropertyName("org_id")]
        public string OrgId { get; set; } = null!;

        [JsonPropertyName("org_name")]
        public string OrgName { get; set; } = null!;

        [JsonPropertyName("org_metadata")]
        public Dictionary<string, object> OrgMetadata { get; set; } = [];

        [JsonPropertyName("user_save_org_name")]
        public string? UrlSafeOrgName { get; set; }

        [JsonPropertyName("user_role")]
        public string? UserAssignedRole { get; set; }

        [JsonPropertyName("inherited_user_roles_plus_current_role")]
        public List<string> UserInheritedRolesPlusCurrentRole { get; set; } = [];

        [JsonPropertyName("user_permissions")]
        public List<string> UserPermissions { get; set; } = [];

        [JsonPropertyName("additional_roles")]
        public List<string> UserAssignedAdditionalRoles { get; set; } = [];
    }
}