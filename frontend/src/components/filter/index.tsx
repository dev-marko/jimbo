/* eslint-disable max-len */
import { Popover, PopoverTrigger, IconButton, Icon, PopoverContent, PopoverArrow, PopoverCloseButton, PopoverHeader, PopoverBody, VStack, FormControl, FormLabel, Input, Select, Button } from '@chakra-ui/react';
import { BiFilter } from 'react-icons/bi';

import { CategoriesEnum } from '~types/forums/categories';
import { Forum } from '~types/forums/forum';

type Props = {
  originalForums: Forum[];
  setForums: (forums: Forum[]) => void;
};

const Filter = ({ originalForums, setForums }: Props) => {
  return (
    <Popover>
      <PopoverTrigger>
        <IconButton icon={<Icon as={BiFilter} width={6} height={6} />} aria-label="Filter button" colorScheme="purple" justifySelf="end" />
      </PopoverTrigger>
      <PopoverContent>
        <PopoverArrow />
        <PopoverCloseButton />
        <PopoverHeader>filter by...</PopoverHeader>
        <PopoverBody>
          <VStack align="end" spacing={4}>
            <FormControl>
              <FormLabel>name</FormLabel>
              <Input onChange={(event) => setForums(originalForums.filter((forum) => forum.name.toLowerCase().includes(event.target.value.toLowerCase())))} />
            </FormControl>
            <FormControl>
              <FormLabel>category</FormLabel>
              <Select onChange={(event) => setForums(originalForums.filter((forum: Forum) => forum.category === Number(event.target.value)))}>
                {
                  Object.keys(CategoriesEnum).map((key) => (
                    <option key={key} value={key}>{CategoriesEnum[key]}</option>
                  ))
                }
              </Select>
            </FormControl>
            <Button
              colorScheme="purple"
              onClick={() => setForums(originalForums)}
            >
              clear
            </Button>
          </VStack>
        </PopoverBody>
      </PopoverContent>
    </Popover>
  );
};

export default Filter;
