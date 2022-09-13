import { Heading, HStack, Icon, IconButton, Spinner, useDisclosure, VStack } from '@chakra-ui/react';
import { useQuery } from '@tanstack/react-query';
import { ReactElement, useEffect, useRef, useState } from 'react';
import { BiPlus } from 'react-icons/bi';

import AuthenticatedLayout from '~components/authenticated-layout';
import CreateForumDrawer from '~components/create-forum-drawer';
import Filter from '~components/filter';
import ForumCard from '~components/forum-card';
import { FORUM_API_URL } from '~constants/api';
import fetcher from '~fetcher';
import { Forum } from '~types/forums/forum';

const Forums = () => {
  const [forums, setForums] = useState<Forum[]>([]);
  const createForumRef = useRef(null);
  const { isOpen, onClose, onOpen } = useDisclosure();
  const { data, isLoading, refetch } = useQuery(['/Forum/sub-forum'], async () => fetcher.get(`${FORUM_API_URL}/Forum/sub-forum`));

  useEffect(() => {
    if (data) {
      setForums(data.data);
    }
  }, [data]);

  if (isLoading) {
    return (
      <Spinner />
    );
  }

  return (
    <VStack align="start" w="full" spacing={4}>
      <HStack mb={10} w="full" justify="space-between">
        <Heading textDecor="underline" textUnderlineOffset={10} size="md">forums</Heading>
        <HStack>
          <IconButton onClick={onOpen} ref={createForumRef} icon={<Icon as={BiPlus} width={6} height={6} />} aria-label="Filter button" colorScheme="purple" justifySelf="end" />
          <Filter originalForums={data?.data} setForums={setForums} />
        </HStack>
      </HStack>
      {
        forums.map((forum: Forum) => (
          <ForumCard key={forum.id} {...forum} />
        ))
      }
      <CreateForumDrawer
        refetch={refetch}
        drawerRef={createForumRef}
        onClose={onClose}
        isOpen={isOpen}
      />
    </VStack>
  );
};

Forums.getLayout = function getLayout(page: ReactElement) {
  return (
    <AuthenticatedLayout>
      {page}
    </AuthenticatedLayout>
  );
};

export default Forums;
