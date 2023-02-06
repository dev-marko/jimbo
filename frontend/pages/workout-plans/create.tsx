import { Button, FormControl, FormLabel, Heading, Input, Stack, Textarea, useDisclosure, VStack } from '@chakra-ui/react';
import { useMutation } from '@tanstack/react-query';
import { useRouter } from 'next/router';
import { FormEvent, ReactElement, useState } from 'react';

import AuthenticatedLayout from '~components/authenticated-layout';
import CreateWorkoutPlanDrawer from '~components/create-workout-plan-drawer';
import { WORKOUT_PLANS_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { Exercise } from '~types/workout-plans/exercise';

const Create = () => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [weekName, setWeekName] = useState('');
  const [sessionName, setSessionName] = useState('');
  const [workoutId, setWorkoutId] = useState('');
  const [exercises, setExercises] = useState<Exercise[]>([
    {
      name: '',
      sets: 0,
      reps: 0,
      restTime: 0,
    },
  ]);

  const { push } = useRouter();
  const { mutate } = useMutation(
    () => fetcher.post(`${WORKOUT_PLANS_API_URL}/TrainingProgram`, {
      name,
      description,
    }),
    {
      onSuccess: ({ data }) => {
        setWorkoutId(data.id);
        onOpen();
      },
    },
  );

  const { mutate: mutateWorkoutSession } = useMutation(
    () => fetcher.post(
      `${WORKOUT_PLANS_API_URL}/TrainingProgram/week`,
      {
        TrainingProgramId: workoutId,
        WeekName: weekName,
        WorkoutSessions: [
          ...exercises.map((exercise) => ({
            SessionName: sessionName,
            Reps: exercise.reps,
            Sets: exercise.sets,
            RestTime: exercise.restTime,
            ExerciseId: exercise.id,
            WeekName: weekName,
            TrainingProgramId: workoutId,
          })),
        ],
      },
    ),
    {
      onSuccess: () => {
        onClose();
        push('/workout-plans');
      },
    },
  );

  const onSubmitFirstForm = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (name && description) {
      mutate();
    }
  };

  const onSubmit = async () => {
    mutateWorkoutSession();
  };

  return (
    <VStack w="full" align={{ base: 'center', md: 'flex-start' }}>
      <Heading size="md">
        New workout plan
      </Heading>
      <form onSubmit={(event) => onSubmitFirstForm(event)}>
        <Stack spacing={6} mt={12}>
          <FormControl>
            <FormLabel htmlFor="name">Name</FormLabel>
            <Input
              variant="filled"
              id="name"
              name="name"
              onChange={(event) => setName(event.target.value)}
              value={name}
            />
          </FormControl>

          <FormControl>
            <FormLabel htmlFor="description">Description</FormLabel>
            <Textarea
              onChange={(event) => setDescription(event.target.value)}
              variant="filled"
              name="description"
              id="description"
              resize="none"
              value={description}
            />
          </FormControl>
          <Button
            type="submit"
            colorScheme="purple"
          >
            Add week
          </Button>
        </Stack>
      </form>
      <CreateWorkoutPlanDrawer
        isOpen={isOpen}
        onClose={onClose}
        exercises={exercises}
        setExercises={setExercises}
        weekName={weekName}
        setWeekName={setWeekName}
        sessionName={sessionName}
        setSessionName={setSessionName}
        onSubmit={onSubmit}
      />
    </VStack>
  );
};

Create.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default Create;
