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
    <div className='mb-8 flex justify-center space-x-4'>
      <LinkedinShareButton
        url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
        title={post.title}
        summary={post.summary}
      >
        <LinkedinIcon size={32} round />
      </LinkedinShareButton>

      <PocketShareButton
        url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
        title={post.title}
        about={post.summary}
      >
        <PocketIcon size={32} round />
      </PocketShareButton>

      <EmailShareButton
        url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
        subject={post.title}
        about={post.summary}
        body='body'
      >
        <EmailIcon size={32} round />
      </EmailShareButton>
    </div>
  )
}
