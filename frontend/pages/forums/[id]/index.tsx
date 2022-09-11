import { Heading, HStack, Icon, IconButton, Spinner, useDisclosure, VStack } from '@chakra-ui/react';
import { dehydrate, QueryClient, useQuery } from '@tanstack/react-query';
import { useRouter } from 'next/router';
import { ReactElement, useEffect, useRef, useState } from 'react';
import { BiPlus } from 'react-icons/bi';

import AuthenticatedLayout from '~components/authenticated-layout';
import CreateTopicDrawer from '~components/create-topic-drawer';
import TopicCard from '~components/topic-card';
import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { Forum } from '~types/forums/forum';
import { Topic } from '~types/topics';

const Subforum = () => {
  const [forum, setForum] = useState<Forum>();
  const createTopicRef = useRef(null);
  const { query: { id } } = useRouter();
  const { isOpen, onClose, onOpen } = useDisclosure();
  const { data, isLoading, refetch } = useQuery(
    [`/Forum/sub-forum/${id}`],
    async () => fetcher.get(`${FORUM_API_URL}/Forum/sub-forum/${id}`),
  );

  useEffect(() => {
    if (data) {
      setForum(data.data);
    }
  }, [data]);

  if (isLoading) {
    return (
      <Spinner />
    );
  }

  return (
    <VStack w="full" align="start" spacing={8}>
      <HStack w="full" justify="space-between">
        <Heading textDecor="underline" textUnderlineOffset={10} size="lg">{forum?.name}</Heading>
        <IconButton onClick={onOpen} ref={createTopicRef} icon={<Icon as={BiPlus} width={6} height={6} />} aria-label="Filter button" colorScheme="purple" justifySelf="end" />
      </HStack>
      {forum?.description && (
        <Heading size="md" fontWeight="normal">{forum.description}</Heading>
      )}
      <VStack w="full">
        {
        forum?.topics.map((topic: Topic) => (
          <TopicCard key={topic.topicId} {...topic} />
        ))
      }
        <CreateTopicDrawer
          isOpen={isOpen}
          onClose={onClose}
          refetch={refetch}
          drawerRef={createTopicRef}
        />
      </VStack>
    </VStack>
  );
};

export async function getStaticProps({ params }: { params: { id: string } }) {
  const { id } = params;

  const queryClient = new QueryClient();
  queryClient.prefetchQuery([`/Forum/sub-forum/${id}`], async () => fetcher.get(`${FORUM_API_URL}/Forum/sub-forum/${id}}`));

  return {
    props: {
      dehydratedState: dehydrate(queryClient),
    },
  };
}

export async function getStaticPaths() {
  const { data: forums } = await fetcher.get(`${FORUM_API_URL}/Forum/sub-forum`);

  return {
    paths: forums.map((forum: Forum) => {
      const { id } = forum;
      return {
        params: {
          id,
        },
      };
    }),
    fallback: false,
  };
}

Subforum.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default Subforum;
