import { createFileRoute, Outlet } from "@tanstack/react-router";

export const Route = createFileRoute("/_public")({
	component: RouteComponent,
});

function RouteComponent() {
	return (
		<div className="flex h-full items-center justify-center">
			<Outlet />
		</div>
	);
}
