/* eslint-disable react/no-array-index-key */
import { Button, Divider, Drawer, DrawerBody, Icon, DrawerCloseButton, DrawerContent, DrawerFooter, DrawerHeader, DrawerOverlay, Flex, FormControl, FormLabel, Heading, IconButton, Input, SlideFade, Stack, VStack, HStack } from '@chakra-ui/react';
import { Dispatch, SetStateAction, useState } from 'react';
import { BiPlus } from 'react-icons/bi';

import AddExerciseForm from '~components/add-exercise-form';
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
            {hasAddedWorkoutSession && (
              <SlideFade in={hasAddedWorkoutSession}>
                <Stack spacing={6} mt={12} bg="gray.50" w="full" p={8} rounded="md">
                  <Stack w="full" spacing={{ base: 8, md: 12 }}>
                    <Stack direction={{ base: 'column', sm: 'row' }} spacing={3} align={{ base: 'start', sm: 'end' }}>
                      <FormControl
                        w={{ base: '100%', sm: 'auto' }}
                        flexGrow="0"
                      >
                        <FormLabel
                          htmlFor="sessionName"
                          w={{ base: '100%', sm: 'auto' }}
                        >
                          Session Name

                        </FormLabel>
                        <Input
                          w={{ base: '100%', sm: 'auto' }}
                          variant="filled"
                          id="sessionName"
                          name="sessionName"
                          onChange={(event) => setSessionName(event.target.value)}
                          value={sessionName}
                        />
                      </FormControl>
                      <IconButton
                        aria-label="Add Exercise"
                        _hover={{
                          background: 'purple.600',
                        }}
                        background="purple.500"
                        icon={<Icon color="white" fontSize="xl" as={BiPlus} />}
                        w={{ base: 'full', sm: 'max-content' }}
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
                      />
                    </Stack>

                    <Divider w="full" />
                    <AddExerciseForm exercises={exercises} setExercises={setExercises} />
                  </Stack>
                </Stack>
              </SlideFade>
            )}
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
