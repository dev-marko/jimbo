import { VStack, FormControl, FormLabel, Input, FormErrorMessage, Button, Alert, AlertIcon } from '@chakra-ui/react';
import { useMutation } from '@tanstack/react-query';
import { Formik, Field } from 'formik';
import { useRouter } from 'next/router';
import { object, string } from 'yup';

import { useAuth } from '~providers/auth-provider';
import { SignInInput } from '~types/authentication/sign-in';

const SignInForm = () => {
  const { query } = useRouter();
  const { signIn } = useAuth();
  const { mutateAsync, isLoading, isError, error } =
    useMutation(['signIn'], async ({ username, password }: SignInInput) => signIn({ username, password }));

  return (
    <Formik
      initialValues={{
        username: query.username as string || '',
        password: '',
      }}
      validationSchema={object({
        username: string().required('Username is required'),
        password: string().required('Password is required'),
      })}
      onSubmit={async (values) => {
        const { username, password } = values;

        mutateAsync({ username, password });
      }}
    >
      {({ handleSubmit, errors, touched }) => (
        <form onSubmit={handleSubmit}>
          <VStack spacing={4} align="flex-start">
            <FormControl isInvalid={!!errors.username && touched.username} isRequired>
              <FormLabel htmlFor="username">
                Username
              </FormLabel>
              <Field as={Input} name="username" variant="filled" />
              <FormErrorMessage>{errors.username}</FormErrorMessage>
            </FormControl>
            <FormControl isInvalid={!!errors.password && touched.password} isRequired>
              <FormLabel htmlFor="password">
                Password
              </FormLabel>
              <Field as={Input} name="password" type="password" variant="filled" />
              <FormErrorMessage>{errors.password}</FormErrorMessage>
            </FormControl>
            <Button disabled={isLoading} type="submit" w="full" colorScheme="purple">Sign in</Button>
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

export default SignInForm;
