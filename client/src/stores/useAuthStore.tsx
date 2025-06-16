import { redirect } from "@tanstack/react-router";
import { toast } from "sonner";
import { create } from "zustand";
import type { components } from "../types/schema";

type User = components["schemas"]["UserDto"];
type LoginRequest = components["schemas"]["LoginRequestDto"];
type LoginResponse = components["schemas"]["LoginResponseDto"];

interface AuthStore {
	profile: User | null;
	authorise: () => Promise<void>;
	login: (req: LoginRequest) => Promise<boolean>;
	logout: () => void;
}

export const useAuthStore = create<AuthStore>((set, get) => ({
	profile: null,
	authorise: async () => {
		if (get().profile) {
			return;
		}

		const token = localStorage.getItem("token");

		if (!token) {
			throw redirect({ to: "/login" });
		}

		const res = await fetch("http://localhost:5005/auth/profile", {
			method: "GET",
			headers: {
				Authorization: `Bearer ${token}`,
				"Content-Type": "application/json",
			},
		});

		if (!res.ok) {
			localStorage.removeItem("token");
			throw redirect({ to: "/login" });
		}

		const profile = (await res.json()) as User;

		set({ profile });

		return;
	},
	login: async (req) => {
		const res = await fetch("http://localhost:5005/auth/login", {
			body: JSON.stringify(req),
			method: "POST",
			headers: { "Content-Type": "application/json" },
		});

		if (!res.ok) {
			toast.error("Login failed");
			return false;
		}

		const data = (await res.json()) as LoginResponse;

		if (!data.accessToken) {
			toast.error("No access token received");
			return false;
		}

		localStorage.setItem("token", data.accessToken);

		set({ profile: data.user });

		toast.success("Login successful");
		return true;
	},
	logout: () => {
		localStorage.removeItem("token");

		set({ profile: null });
	},
}));
