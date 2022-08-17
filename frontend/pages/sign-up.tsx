import { VStack, Box, Text } from "@chakra-ui/react";

import Link from "~components/link";
import SignUpForm from "~components/sign-up-form";

const SignUp = () => {
    return (
        <VStack bg="gray.100" align="center" justify="center" h="100vh">
            <Box bg="white" p={6} rounded="md">
                <SignUpForm />
            </Box>
            <Text p={4}>...or <Link fontWeight="bold" href="/sign-in">sign in with an existing account</Link></Text>
        </VStack>
    )

}

export default SignUp;