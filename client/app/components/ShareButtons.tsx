import siteMetadata from '@/app/utils/siteMetaData'
import {
  EmailIcon,
  EmailShareButton,
  LinkedinIcon,
  LinkedinShareButton,
  PocketIcon,
  PocketShareButton,
} from 'next-share'
import { Post } from '../models'

interface ShareButtonsProps {
  post: Post
}

export default function ShareButtons({ post }: ShareButtonsProps) {
  return (
    <div className='fixed bottom-10 right-10 z-50 flex flex-col items-center space-y-4 rounded-lg border border-gray-100 bg-white p-4 shadow-sm hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700 sm:bottom-10 sm:right-2 sm:p-3 md:bottom-10 md:right-4 lg:p-4'>
      <div className='transform transition duration-150 ease-in-out hover:scale-110'>
        <LinkedinShareButton
          url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
          title={post.title}
          summary={post.summary}
        >
          <LinkedinIcon size={32} round />
        </LinkedinShareButton>
      </div>

      <div className='transform transition duration-150 ease-in-out hover:scale-110'>
        <PocketShareButton
          url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
          title={post.title}
          about={post.summary}
        >
          <PocketIcon size={32} round />
        </PocketShareButton>
      </div>

      <div className='transform transition duration-150 ease-in-out hover:scale-110'>
        <EmailShareButton
          url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
          subject={post.title}
          about={post.summary}
          body='body'
        >
          <EmailIcon size={32} round />
        </EmailShareButton>
      </div>
    </div>
  )
}
