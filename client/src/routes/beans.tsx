import { createFileRoute, useNavigate } from "@tanstack/react-router";
import { beansQueryOptions } from "../data/beans";
import { useQuery } from "@tanstack/react-query";
import { z } from "zod";

const beansSearchSchema = z.object({
	pageIndex: z.number().min(1).catch(1),
});

export const Route = createFileRoute("/beans")({
	component: RouteComponent,
	validateSearch: beansSearchSchema,
	loaderDeps: ({ search }) => ({ pageIndex: search.pageIndex }),
	loader: ({ context: { queryClient }, deps: { pageIndex } }) => {
		return queryClient.ensureQueryData(beansQueryOptions(pageIndex));
	},
});

function RouteComponent() {
	const navigate = useNavigate();
	const { pageIndex } = Route.useSearch();
	const beansQuery = useQuery(beansQueryOptions(pageIndex));

	const handePrevClick = () => {
		navigate({
			from: Route.fullPath,
			search: (prev) => ({
				...prev,
				pageIndex: Math.max(1, prev.pageIndex - 1),
			}),
		});
	};

	const handleNextClick = () => {
		navigate({
			from: Route.fullPath,
			search: (prev) => ({
				...prev,
				pageIndex: Math.min(
					prev.pageIndex + 1,
					beansQuery.data?.totalPages || 1,
				),
			}),
		});
	};

	console.log("Beans Query Data:", beansQuery.data);

	return (
		<div className="p-2 space-y-2">
			<input
				className="bg-gray-200 rounded w-full p-2"
				placeholder="Search for jellybeans"
			/>

			{beansQuery.isLoading || beansQuery.isFetching ? (
				<div>Loading...</div>
			) : (
				beansQuery.data?.items.map((bean) => (
					<div
						key={bean.beanId}
						className="rounded-lg p-2 border-x-2 border-t-2 border-b-4 border-rose-500"
					>
						{bean.flavorName}
					</div>
				))
			)}

			<div className="flex items-center justify-between">
				<button
					disabled={beansQuery.data?.currentPage === 1}
					onClick={handePrevClick}
					type="button"
					className="rounded-lg p-2 bg-rose-500 text-white font-extrabold cursor-pointer"
				>
					Prev
				</button>
				<button
					onClick={handleNextClick}
					type="button"
					className="rounded-lg p-2 bg-rose-500 text-white font-extrabold cursor-pointer"
				>
					Next
				</button>
			</div>
		</div>
	);
}
