import { Box, Container, Heading, VStack } from '@chakra-ui/react';

type Props = {
  children: React.ReactNode;
};

const UnauthenticatedLayout = ({ children }: Props) => {
  return (
    <Box bg="gray.100">
      <Container
        display="flex"
        maxW={{ base: 'container.sm', md: 'container.md' }}
        minH="100vh"
        px={{ base: 4, lg: 0 }}
        centerContent
      >
        <Heading mt={4} alignSelf="start" size="sm">jimbo 💪</Heading>
        <VStack alignItems="stretch" flex={1} w="full" spacing={16}>
          <VStack as="main" flex={1} w="full" spacing={16}>
            {children}
          </VStack>
        </VStack>
      </Container>
    </Box>
  );
};

export default UnauthenticatedLayout;
