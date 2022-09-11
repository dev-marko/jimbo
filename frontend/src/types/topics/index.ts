import { Post } from '~types/posts';

export type Topic = {
  topicId: string;
  ownerUsername: string;
  title: string;
  subforumId: string;
  createdAt: string;
  posts: Post[];
};
