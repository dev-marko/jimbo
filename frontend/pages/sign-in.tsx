import { Box, Text, VStack, } from "@chakra-ui/react";

import Link from "~components/link";
import SignInForm from "~components/sign-in-form";

const SignIn = () => {
    return (
        <VStack bg="gray.100" align="center" justify="center" h="100vh">
            <Box bg="white" p={6} rounded="md">
                <SignInForm />
            </Box>
            <Text p={4}>...or <Link fontWeight="bold" href="/sign-up">create an account</Link></Text>
        </VStack>
    )
}

export default SignIn;