import { Heading, HStack, LinkBox, LinkOverlay, VStack, Text } from '@chakra-ui/react';
import Link from 'next/link';

import { CategoriesEnum } from '~types/forums/categories';
import { Forum } from '~types/forums/forum';

type Props = Forum;

const ForumCard = ({ name, description, category }: Props) => {
  return (
    <LinkBox as="article" w="full">
      <VStack
        alignItems="stretch"
        w="full"
        p={4}
        bg="gray.100"
        _hover={{
          transform: 'scale(1.025, 1.025)',
        }}
        _dark={{
          _hover: {
            bg: 'gray.700',
          },
        }}
        rounded="md"
        transitionDuration="slow"
        transitionProperty="all"
        transitionTimingFunction="ease-out"
      >
        <VStack alignItems="flex-start">
          <Link href={`/blog/${name}`} passHref>
            <LinkOverlay>
              <Heading size="md">{name}</Heading>
            </LinkOverlay>
          </Link>
          <HStack
            divider={(
              <Text mx={2} color="gray.500">
                •
              </Text>
            )}
          >
            <Text color="gray.500" fontSize="sm">
              {CategoriesEnum[category]}
            </Text>
            <Text color="gray.500" fontSize="sm">
              {description}
            </Text>
          </HStack>
        </VStack>
      </VStack>
    </LinkBox>
  );
};

export default ForumCard;
