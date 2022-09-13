export const CategoriesEnum: { [key: string]: string } = {
  1: 'Discussions',
  2: 'Fitness',
  3: 'Intro',
  4: 'Nutrition',
  5: 'Offtopic',
  6: 'Sport',
};

export type Categories = keyof typeof CategoriesEnum;
