import { ReactElement, useState } from 'react';
import { useMutation, useQuery } from '@tanstack/react-query';
import { Button, Divider, Heading, HStack, Icon, Spinner, Text, VStack } from '@chakra-ui/react';
import { useRouter } from 'next/router';
import { AiOutlineDownload } from 'react-icons/ai';

import AuthenticatedLayout from '~components/authenticated-layout';
import { WORKOUT_PLANS_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { WorkoutPlan } from '~types/workout-plans/workout-plan';
import Week from '~types/workout-plans/weeks';

const WorkoutPlanPage = () => {
  const [workoutPlan, setWorkoutPlan] = useState<WorkoutPlan | null>(null);
  const [weeks, setWeeks] = useState<Week[] | null>(null);
  const { query: { id } } = useRouter();

  const { isLoading: isLoadingTrainingProgram } = useQuery(
    [`/TrainingProgram/${id}`],

    async () => fetcher.get(`${WORKOUT_PLANS_API_URL}/TrainingProgram/${id}`),
    {
      onSuccess: (result) => {
        setWorkoutPlan(result.data);
      },
      enabled: !!id,
    },
  );

  const { isLoading: isLoadingWeeks } = useQuery(
    [`/TrainingProgram/${id}/workout-sessions`],
    async () => fetcher.get(`${WORKOUT_PLANS_API_URL}/TrainingProgram/${id}/weeks`),
    {
      onSuccess: (result) => {
        setWeeks(result.data);
      },
      enabled: !!id,
    },
  );

  const { isLoading: isLoadingDownloadPdf, mutate } = useMutation(
    [`/TrainingProgram/${id}/download`],
    async () => fetcher.get(`${WORKOUT_PLANS_API_URL}/TrainingProgram/${id}/download`, {
      responseType: 'blob',
    }),
    {
      onSuccess: (result) => {
        const url = window.URL.createObjectURL(new Blob([result.data]));
        const link = document.createElement('a');

        const fileName = `${workoutPlan?.name}.pdf`;

        link.href = url;
        link.setAttribute('download', fileName);
        document.body.appendChild(link);
        link.click();
      },
    },
  );

  if (isLoadingTrainingProgram || isLoadingWeeks) {
    return (
      <Spinner />
    );
  }

  if (!workoutPlan || !weeks) {
    return null;
  }

  return (
    <VStack align="flex-start" w="full">
      <Text color="blackAlpha.500">
        Workout name:
      </Text>
      <HStack w="full" justify="space-between">
        <Heading size="lg">
          {workoutPlan.name}
        </Heading>
        <Button
          isLoading={isLoadingDownloadPdf}
          size="sm"
          colorScheme="purple"
          leftIcon={(
            <Icon
              fontSize="2xl"
              as={AiOutlineDownload}
            />
          )}
          onClick={() => mutate()}
        >
          Download PDF
        </Button>
      </HStack>
      <Text color="blackAlpha.500">
        Description:
      </Text>
      <Text>
        {workoutPlan.description}
      </Text>
      <Heading size="md" pb={4} pt={4}>
        Weeks:
      </Heading>
      <VStack align="flex-start" w="full">
        {weeks.map((week) => (
          <VStack align="flex-start" w="full" key={week.weekName} shadow="md" background="whiteAlpha.300" p={4}>
            <Heading size="sm">
              Week
              {' '}
              {week.weekName}
            </Heading>
            <Divider borderColor="gray.300" />
            <VStack align="flex-start" w="full">
              {week.workoutSessions.length === 0 && (
                <Text>
                  No workouts found
                </Text>
              )}
              {week.workoutSessions.map((workoutSession) => (
                <VStack align="flex-start" w="full" key={workoutSession.exerciseId}>
                  <Text>
                    {workoutSession.exercise.name}
                    {' '}
                    -
                    {' '}
                    {workoutSession.sets}
                    {' '}
                    sets of
                    {' '}
                    {workoutSession.reps}
                    {' '}
                    reps
                  </Text>
                </VStack>
              ))}
            </VStack>

          </VStack>
        ))}
      </VStack>
    </VStack>
  );
};

WorkoutPlanPage.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default WorkoutPlanPage;
