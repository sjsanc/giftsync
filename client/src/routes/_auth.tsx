import {
	createFileRoute,
	Link,
	Outlet,
	redirect,
} from "@tanstack/react-router";
import { useAuth } from "../stores/auth";
import { CircleUser } from "lucide-react";

export const Route = createFileRoute("/_auth")({
	component: Component,
	beforeLoad: () => {
		const isAuthenticated = useAuth.getState().isAuthenticated;

		// if (!isAuthenticated) {
		// 	throw redirect({
		// 		to: "/login",
		// 	});
		// }
	},
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
