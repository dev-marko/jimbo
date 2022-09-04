import { Box, Text, VStack } from '@chakra-ui/react';
import { ReactElement } from 'react';

import Link from '~components/link';
import SignInForm from '~components/sign-in-form';
import UnauthenticatedLayout from '~components/unauthenticated-layout';

const SignIn = () => {
  return (
    <VStack align="center" justify="center" flex={1} w="full">
      <Box bg="white" p={6} rounded="md">
        <SignInForm />
      </Box>
      <Text p={4}>
        ...or
        {' '}
        <Link fontWeight="bold" href="/sign-up">create an account</Link>
      </Text>
    </VStack>
  );
};

SignIn.getLayout = function getLayout(page: ReactElement) {
  return (
    <UnauthenticatedLayout>
      {page}
    </UnauthenticatedLayout>
  );
};

export default SignIn;
