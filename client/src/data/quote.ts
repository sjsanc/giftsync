import { queryOptions } from "@tanstack/react-query";

const URL = "https://api.wc3.blizzardquotes.com/v1/quotes/random";

export const fetchQuote = async () => {
	const res = await fetch(URL);
	const data = await res.json();

	if (!res.ok) {
		throw new Error(data.message || "Failed to fetch random quote");
	}

	return data;
};

export const quoteQueryOptions = queryOptions({
	queryKey: ["quote"],
	queryFn: fetchQuote,
});
