import { createFileRoute, redirect } from "@tanstack/react-router";
import { useAuth } from "../stores/auth";

export const Route = createFileRoute("/logout")({
	beforeLoad: () => {
		useAuth.getState().logout();

		throw redirect({
			to: "/",
		});
	},
});
