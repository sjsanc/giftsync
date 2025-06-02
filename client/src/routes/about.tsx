import { createFileRoute } from "@tanstack/react-router";
import { create } from "zustand";

export const Route = createFileRoute("/about")({
	component: RouteComponent,
	beforeLoad: () => {
		console.log("About");
	},
});

interface ToggleStore {
	isToggled: boolean;
	toggle: () => void;
}

const useToggleStore = create<ToggleStore>((set) => ({
	isToggled: false,
	toggle: () => set((state) => ({ isToggled: !state.isToggled })),
}));

function RouteComponent() {
	const { isToggled, toggle } = useToggleStore();

	return (
		<div className="p-2">
			<h3>About this website</h3>

			<div className="p-2 border border-black rounded hover:bg-gray-100 space-x-2 w-fit">
				<input
					type="checkbox"
					name="toggle"
					id="toggle"
					checked={isToggled}
					onChange={toggle}
				/>
				<label htmlFor="toggle">Toggle Me</label>
			</div>
		</div>
	);
}
