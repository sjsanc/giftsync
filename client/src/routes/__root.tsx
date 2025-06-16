import { createRootRouteWithContext, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import type { QueryClient } from "@tanstack/react-query";
import { Toaster } from "sonner";

export const Route = createRootRouteWithContext<{ queryClient: QueryClient }>()(
	{
		component: RouteComponent,
	},
);

function RouteComponent() {
	return (
		<>
			<div className="bg-slate-100 text-gray-900 flex flex-col h-screen">
				<Outlet />
				<TanStackRouterDevtools />
				<Toaster richColors />
			</div>
		</>
	);
}
