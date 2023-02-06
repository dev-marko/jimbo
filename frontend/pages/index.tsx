import { Box, Heading, Icon, Spinner, IconButton, chakra, LinkOverlay, LinkBox, Link, VStack, HStack, Text, Stack } from '@chakra-ui/react';
import { BsBoxArrowUpRight } from 'react-icons/bs';
import { useQuery } from '@tanstack/react-query';
import { ReactElement, useState } from 'react';
import { useRouter } from 'next/router';

import AuthenticatedLayout from '~components/authenticated-layout';
import fetcher from '~fetcher';
import { FORUM_API_URL, WORKOUT_PLANS_API_URL } from '~constants/api';
import { Forum } from '~types/forums/forum';
import ForumCard from '~components/forum-card';
import WorkoutPlanCard from '~components/workout-plan-card';
import { WorkoutPlan } from '~types/workout-plans/workout-plan';

import { NextPageWithLayout } from './_app';

const Home: NextPageWithLayout = () => {
  const [mostRecentForums, setMostRecentForums] = useState<Forum[]>([]);
  const [mostRecentWorkoutPlans, setMostRecentWorkoutPlans] = useState<WorkoutPlan[]>([]);
  const { pathname } = useRouter();

  const { isLoading: isLoadingForums } = useQuery([`/Forum/sub-forum-${pathname}`], async () => fetcher.get(`${FORUM_API_URL}/Forum/sub-forum`), {
    onSuccess: ({ data }) => {
      setMostRecentForums(data);
    },
  });

  const { isLoading: isLoadingWorkoutPlans } = useQuery(
    [`/TrainingProgram-${pathname}`],
    async () => fetcher.get(`${WORKOUT_PLANS_API_URL}/TrainingProgram`),
    {
      onSuccess: ({ data }) => {
        setMostRecentWorkoutPlans(data);
      },
    },
  );

  if (isLoadingForums || isLoadingWorkoutPlans) {
    return (
      <Spinner />
    );
  }

  return (
    <>
      <Box>
        <Heading mb={0} size="lg">
          💪 Unleash your fitness potential with
          {' '}
          <chakra.span color="purple.500">Jimbo</chakra.span>
        </Heading>
        <Text fontSize="lg" mt={0}>
          Create custom workout plans tailored to your goals and track your progress with ease.
          Connect with a supportive community of fitness enthusiasts and
          share tips, advice and motivation in our lively forum.
          Get fit and reach new levels of performance with Jimbo today!
        </Text>
      </Box>
      <Stack pb={8} spacing={{ base: 8, md: 2 }} direction={{ base: 'column', md: 'row' }} alignSelf="flex-start" align="flex-start" w="full">
        <Box h="full" w="100%">
          <Box w="100%" display="flex" mb={4}>
            <Heading size="md">
              Most recent forums
              {' '}
            </Heading>
            <LinkBox display="flex" alignItems="center">
              <Link
                href="/forums"
                display="flex"
              >
                <LinkOverlay>
                  <IconButton
                    size="lg"
                    variant="link"
                    icon={<Icon as={BsBoxArrowUpRight} />}
                    aria-label="Follow link button"
                    colorScheme="purple"
                    _hover={{
                      transform: 'scale(1.1)',
                    }}
                  />
                </LinkOverlay>
              </Link>
            </LinkBox>
          </Box>
          <Box>
            <VStack>
              {mostRecentForums.slice(
                mostRecentForums.length - 3,
                mostRecentForums.length,
              ).map((forum: Forum) => (
                <ForumCard key={forum.id} {...forum} />
              ))}
            </VStack>
          </Box>
        </Box>
        <Box w="100%">
          <Box w="100%" display="flex" mb={4}>
            <Heading size="md">
              Most recent workouts plans
              {' '}
            </Heading>
            <LinkBox display="flex" alignItems="center">
              <Link
                href="/workout-plans"
                display="flex"
              >
                <LinkOverlay>
                  <IconButton
                    size="lg"
                    variant="link"
                    icon={<Icon as={BsBoxArrowUpRight} />}
                    aria-label="Follow link button"
                    colorScheme="purple"
                    _hover={{
                      transform: 'scale(1.1)',
                    }}
                  />
                </LinkOverlay>
              </Link>
            </LinkBox>
          </Box>

          <Box>
            <VStack>
              {mostRecentWorkoutPlans.slice(
                mostRecentWorkoutPlans.length - 3,
                mostRecentWorkoutPlans.length,
              ).map((plan: WorkoutPlan) => (
                <WorkoutPlanCard key={plan.id} {...plan} />
              ))}
            </VStack>
          </Box>
        </Box>
      </Stack>
      <Box
        pos="fixed"
        bottom={0}
        p={2}
        background="linear-gradient(to right, #8e2de2, #4a00e0)"
        w="full"
        color="white"
        textAlign="center"
      >
        Our mobile application is coming soon! 🚀
      </Box>
    </>
  );
};

Home.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default Home;
