import { Topic } from '~types/topics';

import { Categories } from './categories';

export type Forum = {
  id: number;
  name: string;
  description: string;
  category: Categories;
  topics: Topic[];
};
