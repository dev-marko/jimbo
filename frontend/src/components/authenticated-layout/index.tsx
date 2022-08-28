import { Box, Container, VStack } from "@chakra-ui/react";
import { PropsWithChildren } from "react";
import Header from "./header";

type Props = PropsWithChildren<{}>

const AuthenticatedLayout = ({ children }: Props) => {
    return (
        <Box bg="gray.50">
            <Header />
            <Container
                display="flex"
                maxW="container.md"
                minH={{ base: 'auto', md: '100vh' }}
                px={{ base: 4, lg: 0 }}
                centerContent
            >
                <VStack alignItems="stretch" flex={1} w="full" spacing={16}>
                    <VStack as="main" flex={1} w="full" spacing={16}>
                        {children}
                    </VStack>
                </VStack>
            </Container>
        </Box>
    )
}

export default AuthenticatedLayout;