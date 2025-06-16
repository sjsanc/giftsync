import { createFileRoute, Link, Outlet } from "@tanstack/react-router";
import { CircleUser } from "lucide-react";
import { useAuthStore } from "../stores/useAuthStore";

export const Route = createFileRoute("/_auth")({
	component: Component,
	beforeLoad: useAuthStore.getState().authorise,
});

function Component() {
	return (
		<>
			<div className="border-b border-gray-900 shadow bg-zinc-900 h-12 flex items-center justify-between">
				<Link
					to="/"
					className="bg-gradient-to-br from-violet-500 to-pink-500 text-transparent bg-clip-text h-full px-2 flex items-center text-2xl font-bold"
				>
					Giftsync
				</Link>

				<div className="text-white font-medium">
					<Link
						to="/circles"
						className="hover:text-purple-400 transition-colors"
					>
						Circles
					</Link>
				</div>

				<div className="flex items-center px-2">
					<Link
						to="/profile"
						className="text-white p-2 hover:bg-white/10 rounded-lg duration-100 cursor-pointer"
					>
						<CircleUser size={26} />
					</Link>
				</div>
			</div>
			<Outlet />
		</>
	);
}
