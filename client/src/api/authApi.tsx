// export const loginFn = async (data: { email: string; password: string }) => {};

// Authentication using JWT tokens
// In the frontend, I send a Login Request with a username and password
// The backend, using Aspnetcore Identity, verifies the password
// It returns a JWT token, a refresh token, and a copy of the User DTO
// The frontend stores the JWT token in localStorage, and the UserDTO in memory
// Whenever an API request is made, the JWT token is included in the Authorization header
// If the JWT token is expire, the backend returns a 401 Unauthorized response
// The frontend then uses the refresh token to request a new JWT token
// (It does this via a custom Axios interceptor)
// The new JWT token is stored in localStorage, and the User DTO is updated in memory
// If the user refreshes the page, the frontend checks localStorage for the JWT token as part of loading the authenticated route
// If it finds one, it uses it to make an API request to get the User DTO
// If the request is successful, the User DTO is stored in memory and the session is authenticated
