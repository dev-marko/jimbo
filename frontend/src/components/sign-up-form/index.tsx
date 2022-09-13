import {
  VStack,
  FormControl,
  FormLabel,
  Input,
  FormErrorMessage,
  Button,
  Alert,
  AlertIcon,
} from '@chakra-ui/react';
import { useMutation } from '@tanstack/react-query';
import { Formik, Field } from 'formik';
import { object, string } from 'yup';

import { useAuth } from '~providers/auth-provider';
import { SignUpInput } from '~types/authentication/sign-up';

const SignUpForm = () => {
  const { signUp } = useAuth();
  const { mutateAsync, isLoading, isError, error, isSuccess } = useMutation(
    ['signIn'],
    async ({ email, username, password }: SignUpInput) => signUp({ email, username, password }),
  );

  return (
    <Formik
      initialValues={{
        email: '',
        username: '',
        password: '',
      }}
      validationSchema={object({
        email: string().required('Email is required'),
        username: string().required('Username is required'),
        password: string().required('Password is required'),
      })}
      onSubmit={async (values) => {
        const { email, username, password } = values;

        mutateAsync({ email, username, password });
      }}
    >
      {({ handleSubmit, errors, touched }) => (
        <form onSubmit={handleSubmit}>
          <VStack spacing={4} align="flex-start">
            <FormControl
              isInvalid={!!errors.username && touched.username}
              isRequired
            >
              <FormLabel htmlFor="username">Username</FormLabel>
              <Field as={Input} name="username" variant="filled" />
              <FormErrorMessage>{errors.username}</FormErrorMessage>
            </FormControl>
            <FormControl isInvalid={!!errors.email && touched.email} isRequired>
              <FormLabel htmlFor="email">Email</FormLabel>
              <Field as={Input} name="email" type="email" variant="filled" />
              <FormErrorMessage>{errors.email}</FormErrorMessage>
            </FormControl>
            <FormControl
              isInvalid={!!errors.password && touched.password}
              isRequired
            >
              <FormLabel htmlFor="password">Password</FormLabel>
              <Field
                as={Input}
                name="password"
                type="password"
                variant="filled"
              />
              <FormErrorMessage>{errors.password}</FormErrorMessage>
            </FormControl>
            <Button
              disabled={isLoading}
              type="submit"
              w="full"
              colorScheme="purple"
            >
              Sign in
            </Button>
            {isSuccess && (
              <Alert status="success">
                <AlertIcon />
                Account created. Redirecting...
              </Alert>
            )}
            {isError && (
              <Alert status="error">
                <AlertIcon />
                {error as string}
              </Alert>
            )}
          </VStack>
        </form>
      )}
    </Formik>
  );
};
export default SignUpForm;
