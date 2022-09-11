/* eslint-disable jsx-a11y/anchor-is-valid */
import { Avatar, Box, Button, Container, Divider, Heading, HStack, Icon, Link, Popover, PopoverArrow, PopoverBody, PopoverContent, PopoverTrigger, VStack } from '@chakra-ui/react';
import NextLink from 'next/link';
import { MdLogout } from 'react-icons/md';

import { useAuth } from '~providers/auth-provider';

const Header = () => {
  const { user, signOut } = useAuth();

  return (
    <HStack
      as="nav"
      position="sticky"
      zIndex="banner"
      top={0}
      alignItems="center"
      justifyContent="space-between"
      w="full"
      mb={16}
      py={3}
      bg="white"
      _dark={{
        bg: 'gray.800',
      }}
      insetX={0}
      transitionDuration="normal"
      transitionProperty="background"
    >
      <Container
        alignItems="center"
        justifyContent="space-between"
        display="flex"
        maxW="container.md"
        px={{ base: 4, lg: 0 }}
      >
        <NextLink href="/" passHref>
          <Link>
            <Heading size="sm">jimbo</Heading>
          </Link>
        </NextLink>
        <HStack alignItems="center" spacing={{ base: 0, md: 2 }}>
          <Popover>
            <PopoverTrigger>
              <Box
                _hover={{
                  bg: 'gray.100',
                }}
                p={1.5}
                rounded="full"
                transition="all 0.2s ease-in"
              >
                <Avatar
                  size="sm"
                />
              </Box>
            </PopoverTrigger>
            <PopoverContent>
              <PopoverArrow />
              <PopoverBody px={0} py={2}>
                <VStack align="start">
                  <HStack spacing={4} p={4}>
                    <Avatar size="sm" />
                    <Heading suppressHydrationWarning size="sm" fontWeight="semibold">{user}</Heading>
                  </HStack>
                  <Divider borderWidth="1px" />
                  <Button variant="ghost" aria-label="Logout button" leftIcon={<Icon as={MdLogout} />} onClick={signOut}>Sign out</Button>
                </VStack>
              </PopoverBody>
            </PopoverContent>
          </Popover>
        </HStack>
      </Container>
    </HStack>
  );
};

export default Header;
