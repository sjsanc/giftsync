import { useAuth } from "@/stores/auth";
import { zodResolver } from "@hookform/resolvers/zod";
import { createFileRoute } from "@tanstack/react-router";
import { useForm } from "react-hook-form";
import { z } from "zod";

export const Route = createFileRoute("/_public/register")({
	component: RouteComponent,
});

const registerFormSchema = z.object({
	username: z.string().min(1, "Username is required"),
	email: z.string().email().min(1, "Email is required"),
	password: z.string().min(1, "Password is required"),
});

type RegisterFormSchema = z.infer<typeof registerFormSchema>;

const defaultValues: RegisterFormSchema = {
	username: "",
	email: "",
	password: "",
};

function RouteComponent() {
	const navigate = Route.useNavigate();
	const auth = useAuth();

	const form = useForm<RegisterFormSchema>({
		resolver: zodResolver(registerFormSchema),
		defaultValues,
	});

	const handleSubmit = form.handleSubmit(async (data: RegisterFormSchema) => {
		auth.register();
		await navigate({ to: "/profile" });
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
				>
					Login
				</Button>
			</div>
		</form>
	);
}
