/* eslint-disable react/no-array-index-key */
import { Button, Divider, Drawer, DrawerBody, DrawerCloseButton, DrawerContent, DrawerFooter, DrawerHeader, DrawerOverlay, Flex, FormControl, FormLabel, Heading, Icon, IconButton, Input, NumberDecrementStepper, NumberIncrementStepper, NumberInput, NumberInputField, NumberInputStepper, SlideFade, Stack } from '@chakra-ui/react';
import { Dispatch, SetStateAction, useState } from 'react';
import { FaTrash } from 'react-icons/fa';

import { Exercise } from '~types/workout-plans/exercise';

type Props = {
  isOpen: boolean;
  onClose: () => void;
  exercises: Exercise[];
  setExercises: Dispatch<SetStateAction<Exercise[]>>;
  weekName: string;
  setWeekName: (weekName: string) => void;
  sessionName: string;
  setSessionName: (sessionName: string) => void;
  onSubmit: () => void;
};

const CreateWorkoutPlanDrawer = ({ isOpen,
  onClose,
  exercises,
  setExercises,
  weekName,
  setWeekName,
  sessionName,
  setSessionName,
  onSubmit,
}: Props) => {
  const [hasAddedWorkoutSession, setHasAddedWorkoutSession] = useState(false);

  return (
    <Drawer
      size="full"
      isOpen={isOpen}
      placement="right"
      onClose={onClose}
    >
      <DrawerOverlay />
      <DrawerContent px={{ base: 0, md: 10 }} py={{ base: 0, md: 4 }}>
        <DrawerCloseButton />
        <DrawerHeader><Heading>Add new week</Heading></DrawerHeader>
        <DrawerBody>
          <form
            onSubmit={() => onSubmit()}
            style={{
              width: '100%',
            }}
          >
            <Stack spacing={6} mt={12} w="max-content">
              <FormControl>
                <FormLabel htmlFor="weekName">Week Name</FormLabel>
                <Input
                  variant="filled"
                  id="weekName"
                  name="weekName"
                  onChange={(event) => setWeekName(event.target.value)}
                  value={weekName}
                />
              </FormControl>
              <Button onClick={() => setHasAddedWorkoutSession(true)}>Add Workout Session</Button>
            </Stack>
            <SlideFade in={hasAddedWorkoutSession}>
              <Stack spacing={6} mt={12} bg="gray.50" w="full" p={8} rounded="md" transition="all">
                <Stack w="max-content" gap={4}>
                  <FormControl>
                    <FormLabel htmlFor="sessionName">Session Name</FormLabel>
                    <Input
                      w="max-content"
                      variant="filled"
                      id="sessionName"
                      name="sessionName"
                      onChange={(event) => setSessionName(event.target.value)}
                      value={sessionName}
                    />
                  </FormControl>
                  <Button
                    colorScheme="purple"
                    w="max-content"
                    onClick={() => {
                      setExercises([
                        ...exercises,
                        {
                          name: '',
                          sets: 0,
                          reps: 0,
                          restTime: 0,
                        },
                      ]);
                    }}
                  >
                    Add Exercise
                  </Button>
                  <Stack gap={6}>
                    {
                      exercises.map((_, index) => (
                        <SlideFade in key={index}>
                          <Stack key={index} w="max-content" direction={{ base: 'column', md: 'row' }} align="flex-end">
                            <FormControl>
                              <FormLabel htmlFor={`exercises.${index}.name`}>Exercise Name</FormLabel>
                              <Input
                                w="auto"
                                variant="filled"
                                id={`exercises.${index}.name`}
                                name={`exercises.${index}.name`}
                                onChange={(event) => {
                                  setExercises((prevExercises) => {
                                    const newExercises = [...prevExercises];
                                    newExercises[index].name = event.target.value;
                                    return newExercises;
                                  });
                                }}
                                value={exercises[index].name}
                              />
                            </FormControl>
                            <FormControl>
                              <FormLabel htmlFor={`exercises.${index}.sets`}>Sets</FormLabel>
                              <NumberInput
                                w="auto"
                                variant="filled"
                                id={`exercises.${index}.sets`}
                                name={`exercises.${index}.sets`}
                                onChange={(event) => {
                                  setExercises((prevExercises) => {
                                    const newExercises = [...prevExercises];
                                    newExercises[index].sets = Number(event);
                                    return newExercises;
                                  });
                                }}
                                value={exercises[index].sets}
                              >
                                <NumberInputField />
                                <NumberInputStepper>
                                  <NumberIncrementStepper />
                                  <NumberDecrementStepper />
                                </NumberInputStepper>
                              </NumberInput>
                            </FormControl>
                            <FormControl>
                              <FormLabel htmlFor={`exercises.${index}.reps`}>Reps</FormLabel>
                              <NumberInput
                                w="auto"
                                variant="filled"
                                id={`exercises.${index}.reps`}
                                name={`exercises.${index}.reps`}
                                onChange={(event) => {
                                  setExercises((prevExercises) => {
                                    const newExercises = [...prevExercises];
                                    newExercises[index].reps = Number(event);
                                    return newExercises;
                                  });
                                }}
                                value={exercises[index].reps}
                              >
                                <NumberInputField />
                                <NumberInputStepper>
                                  <NumberIncrementStepper />
                                  <NumberDecrementStepper />
                                </NumberInputStepper>
                              </NumberInput>
                            </FormControl>
                            <FormControl>
                              <FormLabel htmlFor={`exercises.${index}.restTime`}>Rest Time</FormLabel>
                              <NumberInput
                                w="auto"
                                variant="filled"
                                id={`exercises.${index}.restTime`}
                                name={`exercises.${index}.restTime`}
                                onChange={(event) => {
                                  setExercises((prevExercises) => {
                                    const newExercises = [...prevExercises];
                                    newExercises[index].restTime = Number(event);
                                    return newExercises;
                                  });
                                }}
                                value={exercises[index].restTime}
                              >
                                <NumberInputField />
                                <NumberInputStepper>
                                  <NumberIncrementStepper />
                                  <NumberDecrementStepper />
                                </NumberInputStepper>
                              </NumberInput>
                            </FormControl>
                            <IconButton
                              w={{ base: 'full', md: 'auto' }}
                              onClick={() => {
                                setExercises((prevExercises) => {
                                  const newExercises = [...prevExercises];
                                  newExercises.splice(index, 1);
                                  return newExercises;
                                });
                              }}
                              justifySelf="center"
                              aria-label="Delete exercise"
                              icon={<Icon fontSize="md" as={FaTrash} color="red.500" />}
                            />
                          </Stack>
                        </SlideFade>
                      ))
                    }
                  </Stack>
                </Stack>
              </Stack>
            </SlideFade>
          </form>
        </DrawerBody>
        <DrawerFooter display="flex" flexDir="column" p={0}>
          <Divider />
          <Flex w="full" justify="end" p={4}>
            <Button variant="outline" mr={3} onClick={onClose}>
              Cancel
            </Button>
            <Button colorScheme="purple" onClick={() => onSubmit()}>Save</Button>
          </Flex>
        </DrawerFooter>
      </DrawerContent>
    </Drawer>
  );
};

export default CreateWorkoutPlanDrawer;
