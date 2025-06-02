import { createFileRoute } from "@tanstack/react-router";
import { quoteQueryOptions } from "../data/quote";
import { useQuery } from "@tanstack/react-query";

export const Route = createFileRoute("/quote")({
	component: RouteComponent,
	loader: ({ context: { queryClient } }) => {
		return queryClient.ensureQueryData(quoteQueryOptions);
	},
});

function RouteComponent() {
	const quoteQuery = useQuery(quoteQueryOptions);

	return (
		<div className="p-2">
			{quoteQuery.isLoading || quoteQuery.isFetching
				? "Loading..."
				: quoteQuery.data.value}
		</div>
	);
}
