import { Button, Field, Heading, Input, Text } from "@chakra-ui/react";
import { createFileRoute } from "@tanstack/react-router";
import { useAuthStore } from "../stores/useAuthStore";
import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";

export const Route = createFileRoute("/_auth/profile")({
	component: RouteComponent,
});

function RouteComponent() {
	return (
		<div className="w-[1100px] mx-auto py-4 flex flex-col space-y-4">
			<UpdateUsernameBox />
			<UpdateEmailBox />
		</div>
	);
}

const UpdateUsernameBox = () => {
	const authStore = useAuthStore();
	const profile = authStore.profile;

	const form = useForm({
		defaultValues: {
			username: profile?.name || "",
		},
		resolver: zodResolver(
			z.object({
				username: z.string().min(1, "Username is required"),
			}),
		),
		mode: "onTouched",
	});

	const updateUsernameMutator = useMutation({
		mutationFn: async (props: {
			userId: number | undefined;
			username: string;
		}) => {
			const token = localStorage.getItem("token");

			const res = await fetch(
				`http://localhost:5005/user/${props.userId}/username`,
				{
					method: "PUT",
					body: JSON.stringify({ username: props.username }),
					headers: {
						Authorization: `Bearer ${token}`,
						"Content-Type": "application/json",
					},
				},
			);

			if (!res.ok) {
				throw new Error("Failed to update username.");
			}
		},
		onSuccess: () => {
			toast.success("Username updated successfully");

			form.reset({
				username: form.getValues("username"),
			});
		},
	});

	const handleSubmit = form.handleSubmit(async (data) => {
		await updateUsernameMutator.mutateAsync({
			userId: profile?.id,
			username: data.username,
		});
	});

	return (
		<form
			onSubmit={handleSubmit}
			className="bg-white border border-gray-300 rounded p-4 grid"
			style={{ gridTemplateColumns: "1fr 2fr" }}
		>
			<div>
				<Heading>Username</Heading>
				<Text textStyle="sm">Update your username.</Text>
			</div>
			<div className="flex flex-col space-y-2">
				<Field.Root invalid={!!form.formState.errors.username}>
					<Field.Label>Username</Field.Label>
					<Input {...form.register("username")} />
					<Field.ErrorText>
						{form.formState.errors?.username?.message}
					</Field.ErrorText>
				</Field.Root>
				<div className="ml-auto">
					<Button
						type="submit"
						disabled={!form.formState.isValid || !form.formState.isDirty}
						loading={updateUsernameMutator.isPending}
					>
						Save
					</Button>
				</div>
			</div>
		</form>
	);
};

const UpdateEmailBox = () => {
	const authStore = useAuthStore();
	const profile = authStore.profile;

	const form = useForm({
		defaultValues: {
			email: profile?.email || "",
			confirmEmail: "",
		},
		resolver: zodResolver(
			z
				.object({
					email: z.string().email("Invalid email address"),
					confirmEmail: z.string().email("Invalid email address"),
				})
				.refine((data) => data.email === data.confirmEmail, {
					message: "Emails do not match",
					path: ["confirmEmail"],
				}),
		),
		mode: "onTouched",
	});

	const updateEmailMutator = useMutation({
		mutationFn: async (props: {
			userId: number | undefined;
			email: string | undefined;
		}) => {
			const token = localStorage.getItem("token");

			const res = await fetch(
				`http://localhost:5005/user/${props.userId}/email`,
				{
					method: "PUT",
					body: JSON.stringify({ email: props.email }),
					headers: {
						Authorization: `Bearer ${token}`,
						"Content-Type": "application/json",
					},
				},
			);

			if (!res.ok) {
				throw new Error("Failed to update email.");
			}
		},
		onSuccess: () => {
			toast.success("Email updated successfully");

			form.reset({
				email: form.getValues("email"),
				confirmEmail: "",
			});
		},
	});

	const handleSubmit = form.handleSubmit(async (data) => {
		await updateEmailMutator.mutateAsync({
			userId: profile?.id,
			email: data.email,
		});
	});

	return (
		<form
			onSubmit={handleSubmit}
			className="bg-white border border-gray-300 rounded p-4 grid"
			style={{ gridTemplateColumns: "1fr 2fr" }}
		>
			<div>
				<Heading>Email</Heading>
				<Text textStyle="sm">Update your email.</Text>
			</div>
			<div className="flex flex-col space-y-2">
				<Field.Root invalid={!!form.formState.errors.email}>
					<Field.Label>Email</Field.Label>
					<Input {...form.register("email")} />
					<Field.ErrorText>
						{form.formState.errors?.email?.message}
					</Field.ErrorText>
				</Field.Root>
				<Field.Root invalid={!!form.formState.errors.confirmEmail}>
					<Field.Label>Confirm Email</Field.Label>
					<Input {...form.register("confirmEmail")} />
					<Field.ErrorText>
						{form.formState.errors?.confirmEmail?.message}
					</Field.ErrorText>
				</Field.Root>
				<div className="ml-auto">
					<Button
						type="submit"
						disabled={!form.formState.isValid || updateEmailMutator.isPending}
						loading={updateEmailMutator.isPending}
					>
						Save
					</Button>
				</div>
			</div>
		</form>
	);
};
