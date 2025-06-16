// import { useAuth } from "@/stores/useAuthStore";
// import axios from "axios";

// const api = axios.create({
// 	baseURL: "http://localhost:5005",
// });

// api.interceptors.request.use((config) => {
// 	const token = localStorage.getItem("accessToken");
// 	if (token) {
// 		config.headers.Authorization = `Bearer ${token}`;
// 	}
// 	return config;
// });

// api.interceptors.response.use(
// 	(response) => response,
// 	async (error) => {
// 		if (error.response.status === 401) {
// 			const refreshToken = localStorage.getItem("refreshToken");
// 			if (refreshToken) {
// 				const response = await axios.post("/refresh-token", { refreshToken });
// 				const newToken = response.data.token;

// 				localStorage.setItem("authToken", newToken);
// 				useAuth.getState().login(newToken);

// 				error.config.headers.Authorization = `Bearer ${newToken}`;
// 				return axios(error.config);
// 			}
// 		}
// 		return Promise.reject(error);
// 	},
// );

// export default api;
