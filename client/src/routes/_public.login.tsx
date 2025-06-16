import { createFileRoute } from "@tanstack/react-router";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { Button, Field, Input, Text } from "@chakra-ui/react";
import { useMutation } from "@tanstack/react-query";
import { useAuthStore } from "../stores/useAuthStore";

export const Route = createFileRoute("/_public/login")({
	component: RouteComponent,
});

const loginFormSchema = z.object({
	email: z.string().email().min(1, "Email is required"),
	password: z.string().min(1, "Password is required"),
});

type LoginFormSchema = z.infer<typeof loginFormSchema>;

const defaultValues: LoginFormSchema = {
	email: "",
	password: "",
};

function RouteComponent() {
	const navigate = Route.useNavigate();
	const authStore = useAuthStore();

	const mutation = useMutation({
		mutationFn: authStore.login,
	});

	const form = useForm<LoginFormSchema>({
		resolver: zodResolver(loginFormSchema),
		defaultValues,
	});

	const handleSubmit = form.handleSubmit(async (data: LoginFormSchema) => {
		const result = await mutation.mutateAsync(data);
		if (result) {
			navigate({ to: "/" });
		}
	});

	return (
		<form
			onSubmit={handleSubmit}
			className="flex flex-col w-80 divide-y divide-gray-200 bg-white rounded-lg border border-gray-200 shadow-lg"
		>
			<div className="bg-gradient-to-br from-violet-500 to-pink-500 text-transparent bg-clip-text font-bold text-3xl p-4">
				GiftSync
			</div>

			<div className="p-4 space-y-2">
				<Text>Please log in</Text>

				<Field.Root invalid={!!form.formState.errors.email}>
					<Field.Label>Email</Field.Label>
					<Input {...form.register("email")} />
					<Field.ErrorText>
						{form.formState.errors?.email?.message}
					</Field.ErrorText>
				</Field.Root>
				<Field.Root invalid={!!form.formState.errors.password}>
					<Field.Label>Password</Field.Label>
					<Input {...form.register("password")} type="password" />
					<Field.ErrorText>
						{form.formState.errors?.password?.message}
					</Field.ErrorText>
				</Field.Root>
			</div>
			<div className="p-4">
				<Button
					type="submit"
					colorPalette="purple"
					variant="solid"
					width={"full"}
					loading={mutation.isPending}
				>
					Login
				</Button>
			</div>
		</form>
	);
}
