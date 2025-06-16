import { Avatar, AvatarGroup, Button, Card, Heading } from "@chakra-ui/react";
import { queryOptions, useSuspenseQuery } from "@tanstack/react-query";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_auth/circles")({
	component: RouteComponent,
});

const circlesQueryOptions = queryOptions({
	queryKey: ["circles"],
	queryFn: async () => {
		const token = localStorage.getItem("token");

		const res = await fetch("http://localhost:5005/circle", {
			method: "GET",
			headers: {
				Authorization: `Bearer ${token}`,
				"Content-Type": "application/json",
			},
		});

		if (!res.ok) {
			throw new Error("Failed to fetch circles.");
		}

		return res.json();
	},
});

function RouteComponent() {
	const circlesQuery = useSuspenseQuery(circlesQueryOptions);
	const circles = circlesQuery.data;

	return (
		<div className="w-[1100px] mx-auto py-4 flex flex-col space-y-4">
			<Heading>Your Circles</Heading>
			<Button>Create Circle</Button>

			{circles.map((circle) => ())}

			<Card.Root variant="elevated">
				<Card.Header>
					<div className="flex items-center justify-between">
						<Heading size="lg">Scheepers Family</Heading>
						<AvatarGroup>
							<Avatar.Root>
								<Avatar.Fallback name="Uchiha Sasuke" />
								<Avatar.Image src="https://cdn.myanimelist.net/r/84x124/images/characters/9/131317.webp?s=d4b03c7291407bde303bc0758047f6bd" />
							</Avatar.Root>

							<Avatar.Root>
								<Avatar.Fallback name="Baki Ani" />
								<Avatar.Image src="https://cdn.myanimelist.net/r/84x124/images/characters/7/284129.webp?s=a8998bf668767de58b33740886ca571c" />
							</Avatar.Root>
						</AvatarGroup>
					</div>
				</Card.Header>
				<Card.Body>
					<Button>Invite to Circle</Button>
					<Button>View Next Event</Button>
					<Button>Create Event</Button>
					<Button>Delete Circle</Button>
				</Card.Body>
				<Card.Footer>
					<div />
				</Card.Footer>
			</Card.Root>
		</div>
	);
}
