import type { components } from "@/types/schema";
import { toast } from "sonner";
import { create } from "zustand";

type User = components["schemas"]["UserDto"];
type LoginRequest = components["schemas"]["LoginRequestDto"];

interface AuthStore {
	isAuthenticated: boolean;
	user: User;
	login: (req: LoginRequest) => void;
	logout: () => void;
}

export const useAuth = create<AuthStore>((set) => ({
	isAuthenticated: false,
	user: null,
	login: async (req) => {
		const res = await fetch("http://localhost:5005/user/login", {
			body: JSON.stringify(req),
			method: "POST",
			headers: { "Content-Type": "application/json" },
		});

		if (!res.ok) {
			toast.error("Login failed");
			return;
		}

		const data = await res.json();

		set({
			isAuthenticated: true,
			user: data as User,
		});
	},
	logout: () => set({ isAuthenticated: false, user: null }),
}));
