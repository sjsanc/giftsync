import { queryOptions } from "@tanstack/react-query";

const URL = "https://jellybellywikiapi.onrender.com/api/beans";

export interface BeanQueryResponse {
	currentPage: number;
	items: Bean[];
	pageSize: number;
	totalCount: number;
	totalPages: number;
}

interface Bean {
	beanId: number;
	backgroundColor: string;
	flavorName: string;
	imageUrl: string;
}

export const fetchBeans = async (pageIndex: number) => {
	const res = await fetch(`${URL}?pageIndex=${pageIndex}`);
	const data = await res.json();

	if (!res.ok) {
		throw new Error(data.message || "Failed to fetch random quote");
	}

	return data;
};

export const beansQueryOptions = (pageIndex: number) =>
	queryOptions<BeanQueryResponse>({
		queryKey: ["beans", pageIndex],
		queryFn: () => fetchBeans(pageIndex),
	});
