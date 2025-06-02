import { createFileRoute } from "@tanstack/react-router";
import { useAuth } from "../stores/auth";
import { CalendarDays } from "lucide-react";

export const Route = createFileRoute("/_auth/")({
	component: RouteComponent,
});

function RouteComponent() {
	const auth = useAuth();

	return (
		<div className="flex flex-col space-y-4 container mx-auto py-4">
			<div className="flex flex-col space-y-2">
				<div className="font-semibold">Upcoming Events</div>
				<div className="grid grid-cols-2 gap-4">
					<div className="bg-white border border-gray-200 rounded-lg shadow p-2">
						<div className="font-bold">Christmas 2026</div>
						<div className="text-sm text-gray-600 flex items-center gap-1">
							<CalendarDays size={18} />
							December 25, 2026
						</div>
						<div>Participants</div>
					</div>
				</div>
			</div>

			<div className="flex flex-col space-y-2">
				<div className="font-semibold">Past Events</div>
				<div className="grid grid-cols-2 gap-4">
					<div className="bg-white border border-gray-200 rounded-lg shadow p-2">
						<div className="font-bold">Christmas 2026</div>
						<div className="text-sm text-gray-600 flex items-center gap-1">
							<CalendarDays size={18} />
							December 25, 2025
						</div>
						<div>Participants</div>
					</div>
				</div>
			</div>
		</div>
	);
}
