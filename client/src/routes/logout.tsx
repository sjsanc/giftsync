import { createFileRoute, redirect } from "@tanstack/react-router";
import { useAuthStore } from "../stores/useAuthStore";

export const Route = createFileRoute("/logout")({
	beforeLoad: () => {
		useAuthStore.getState().logout();

		throw redirect({
			to: "/",
		});
	},
});
