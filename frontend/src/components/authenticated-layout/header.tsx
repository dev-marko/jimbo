/* eslint-disable jsx-a11y/anchor-is-valid */
import { Button, Container, Heading, HStack, Icon, Link } from '@chakra-ui/react';
import NextLink from 'next/link';
import { MdLogout } from 'react-icons/md';

import { useAuth } from '~providers/auth-provider';

const Header = () => {
  const { signOut } = useAuth();

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
          <Button variant="ghost" size="sm" leftIcon={<Icon as={MdLogout} />} onClick={signOut}>
            Logout
          </Button>
        </HStack>
      </Container>
    </HStack>
  );
};

export default Header;
