import { Heading, HStack, Icon, IconButton, SimpleGrid, Spinner, VStack } from '@chakra-ui/react';
import { useQuery } from '@tanstack/react-query';
import { useRouter } from 'next/router';
import { ReactElement, useState } from 'react';
import { BiPlus } from 'react-icons/bi';

import AuthenticatedLayout from '~components/authenticated-layout';
import WorkoutPlanCard from '~components/workout-plan-card';
import { WORKOUT_PLANS_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { WorkoutPlan } from '~types/workout-plans/workout-plan';

const WorkoutPlans = () => {
  const [workoutPlans, setWorkoutPlans] = useState<WorkoutPlan[]>([]);

  const { push } = useRouter();
  const { isLoading } = useQuery(
    ['/TrainingProgram'],
    async () => fetcher.get(`${WORKOUT_PLANS_API_URL}/TrainingProgram`),
    {
      onSuccess: (result) => {
        setWorkoutPlans(result.data);
      },
    },
  );

  if (isLoading) {
    return (
      <Spinner />
    );
  }

  return (
    <VStack align="start" w="full" spacing={4}>
      <HStack mb={10} w="full" justify="space-between">
        <Heading textDecor="underline" textUnderlineOffset={10} size="md">workout plans</Heading>
        <HStack>
          <IconButton onClick={() => push('/workout-plans/create')} icon={<Icon as={BiPlus} width={6} height={6} />} aria-label="Filter button" colorScheme="purple" justifySelf="end" />
        </HStack>
      </HStack>
      <SimpleGrid columns={3} spacing={10}>
        {
          workoutPlans.map((plan: WorkoutPlan) => (
            <WorkoutPlanCard key={plan.id} {...plan} />
          ))
        }
      </SimpleGrid>
    </VStack>
  );
};

WorkoutPlans.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default WorkoutPlans;
