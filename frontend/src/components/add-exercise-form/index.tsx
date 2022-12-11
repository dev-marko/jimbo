/* eslint-disable react/no-array-index-key */
import { Divider, FormControl, FormLabel, Icon, IconButton, NumberDecrementStepper, NumberIncrementStepper, NumberInput, NumberInputField, NumberInputStepper, SlideFade, Stack } from '@chakra-ui/react';
import { AutoComplete, AutoCompleteInput, AutoCompleteItem, AutoCompleteList } from '@choc-ui/chakra-autocomplete';
import { useQuery } from '@tanstack/react-query';
import { Dispatch, SetStateAction, useState, useMemo } from 'react';
import { FaTrash } from 'react-icons/fa';

import { WORKOUT_PLANS_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { Exercise } from '~types/workout-plans/exercise';

type Props = {
  exercises: Exercise[];
  setExercises: Dispatch<SetStateAction<Exercise[]>>;
};

const AddExerciseForm = ({ exercises, setExercises }: Props) => {
  const [availableExercises, setAvailableExercises] = useState<Exercise[]>([]);

  const availableExercisesMemo = useMemo(() => availableExercises, [availableExercises]);

  const { isLoading } = useQuery(['/Exercise'], async () => fetcher.get(`${WORKOUT_PLANS_API_URL}/Exercise`), {
    onSuccess: ({ data }) => {
      setAvailableExercises(data);
    },
  });

  return (
    <Stack
      gap={6}
      spacing={8}
      mt={32}
      divider={
        <Divider />
      }
    >
      {
        exercises.map((_, index) => (
          <SlideFade in key={index}>
            <Stack position="relative" key={index} w="auto" direction={{ base: 'column', md: 'row' }} align="flex-end" spacing={4}>
              <FormControl>
                <FormLabel htmlFor={`exercises.${index}.name`}>Name</FormLabel>
                <AutoComplete
                  rollNavigation
                  onChange={(event) => {
                    setExercises((prevExercises) => {
                      const newExercises = [...prevExercises];
                      const name = event;

                      const filteredExercise =
                        availableExercises.find((exercise) => exercise.name === name);

                      newExercises[index].name = name;
                      newExercises[index].id = filteredExercise?.id || '';
                      return newExercises;
                    });
                  }}
                >
                  <AutoCompleteInput
                    w="full"
                    isDisabled={isLoading}
                    variant="filled"
                    placeholder="Search exercises..."
                    autoFocus

                  />
                  <AutoCompleteList>

                    {availableExercisesMemo &&
                      availableExercisesMemo.map(({ name, id }) => (
                        <AutoCompleteItem
                          key={`option-${id}`}
                          id={id}
                          value={`${name}`}
                          textTransform="capitalize"
                        />
                      ))}
                  </AutoCompleteList>
                </AutoComplete>
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
                position="absolute"
                top={-8}
                right={0}
                variant="ghost"
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
  );
};

export default AddExerciseForm;
