import { Heading, HStack, LinkBox, LinkOverlay, VStack, Text } from '@chakra-ui/react';
import Link from 'next/link';

import { WorkoutPlan } from '~types/workout-plans/workout-plan';

type Props = WorkoutPlan;

const WorkoutPlanCard = ({ id, name, description }: Props) => {
  return (
    <LinkBox as="article" w="full">
      <VStack
        h="full"
        alignItems="stretch"
        w="full"
        p={4}
        bg="white"
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
          <Link
            href={{
              pathname: '/forums/[id]',
              query: { id },
            }}
            passHref
          >
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
              {description}
            </Text>
          </HStack>
        </VStack>
      </VStack>
    </LinkBox>
  );
};

export default WorkoutPlanCard;
