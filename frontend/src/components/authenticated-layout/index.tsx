import { Box, Container, VStack } from '@chakra-ui/react';

import Header from './header';
import MobileNavigation from './mobile-navigation';

type Props = {
  children: React.ReactNode;
};

const AuthenticatedLayout = ({ children }: Props) => {
  return (
    <Box bg="gray.50" height="100vh" overflow="auto">
      <Header />
      <Container
        display="flex"
        maxW="container.md"
        px={{ base: 4, lg: 0 }}
        centerContent
        flex={1}
      >
        <VStack alignItems="stretch" w="full" spacing={16}>
          <VStack as="main" w="full" spacing={16}>
            {children}
          </VStack>
        </VStack>
      </Container>
    </Box>
  );
};

export default AuthenticatedLayout;
