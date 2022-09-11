import { LinkBox, VStack, LinkOverlay, Heading, HStack, Text } from '@chakra-ui/react';
import Link from 'next/link';
import { format } from 'timeago.js';

import { Topic } from '~types/topics';

type Props = Topic;

const TopicCard = ({ title, subforumId: id, topicId, createdAt, ownerUsername }: Props) => {
  return (
    <LinkBox as="article" w="full">
      <VStack
        alignItems="stretch"
        w="full"
        p={{ base: 0, md: 4 }}
        _hover={{
          bg: 'gray.100',
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
          <Link
            href={{
              pathname: '/forums/[id]/topics/[topicId]',
              query: { id, topicId },
            }}
            passHref
          >
            <LinkOverlay>
              <Heading size="md">{title}</Heading>
            </LinkOverlay>
          </Link>
          <HStack>
            <Text color="gray.500" fontSize="sm">
              {format(createdAt)}
              {' '}
              by
              {' '}
              {ownerUsername}
            </Text>
            <Text color="gray.500" fontSize="sm" />
          </HStack>
        </VStack>

      </VStack>
    </LinkBox>
  );
};

export default TopicCard;
