import React, { useState } from 'react'
import siteMetadata from '@/app/utils/siteMetaData'
import {
  EmailIcon,
  EmailShareButton,
  LinkedinIcon,
  LinkedinShareButton,
  PocketIcon,
  PocketShareButton,
  RedditIcon,
  RedditShareButton,
  TelegramIcon,
  TelegramShareButton,
  TwitterIcon,
  TwitterShareButton,
  WhatsappIcon,
  WhatsappShareButton,
} from 'next-share'
import { Post } from '@/app/models'

interface ShareButtonsProps {
  post: Post
}

export default function ShareButtons({ post }: ShareButtonsProps) {
  const [isHovered, setIsHovered] = useState(false)

  return (
    <div
      className='group relative flex cursor-pointer flex-col items-center'
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <svg
        className='z-10 h-6 w-6 text-gray-800 dark:text-white'
        aria-hidden='true'
        xmlns='http://www.w3.org/2000/svg'
        fill='currentColor'
        viewBox='0 0 24 24'
      >
        <path d='M17.5 3A3.5 3.5 0 0 0 14 7L8.1 9.8A3.5 3.5 0 0 0 2 12a3.5 3.5 0 0 0 6.1 2.3l6 2.7-.1.5a3.5 3.5 0 1 0 1-2.3l-6-2.7a3.5 3.5 0 0 0 0-1L15 9a3.5 3.5 0 0 0 6-2.4c0-2-1.6-3.5-3.5-3.5Z' />
      </svg>
      {isHovered && (
        <div className='absolute top-full mb-2 flex flex-col items-center space-y-4 rounded-lg border border-gray-100 bg-white p-4 opacity-100 shadow-sm transition-opacity duration-300 hover:bg-gray-100 hover:text-cyan-700 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700'>
          <div className='transform transition duration-150 ease-in-out hover:scale-110'>
            <LinkedinShareButton
              url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
              title={post.title}
              summary={post.summary}
              aria-label='Compartilhar no LinkedIn'
            >
              <LinkedinIcon size={32} round />
            </LinkedinShareButton>
          </div>

          <div className='transform transition duration-150 ease-in-out hover:scale-110'>
            <PocketShareButton
              url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
              title={post.title}
              about={post.summary}
              aria-label='Salvar no Pocket'
            >
              <PocketIcon size={32} round />
            </PocketShareButton>
          </div>

          <div className='transform transition duration-150 ease-in-out hover:scale-110'>
            <EmailShareButton
              url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
              subject={post.title}
              about={post.summary}
              aria-label='Compartilhar por Email'
            >
              <EmailIcon size={32} round />
            </EmailShareButton>
          </div>

          <div className='transform transition duration-150 ease-in-out hover:scale-110'>
            <RedditShareButton
              url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
              title={post.title}
              aria-label='Compartilhar no Reddit'
            >
              <RedditIcon size={32} round />
            </RedditShareButton>
          </div>

          <div className='transform transition duration-150 ease-in-out hover:scale-110'>
            <TelegramShareButton
              url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
              title={post.title}
              aria-label='Compartilhar no Telegram'
            >
              <TelegramIcon size={32} round />
            </TelegramShareButton>
          </div>

          <div className='transform transition duration-150 ease-in-out hover:scale-110'>
            <TwitterShareButton
              url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
              title={post.title}
              aria-label='Compartilhar no Twitter/X'
            >
              <TwitterIcon size={32} round />
            </TwitterShareButton>
          </div>

          <div className='transform transition duration-150 ease-in-out hover:scale-110'>
            <WhatsappShareButton
              url={`${siteMetadata.siteUrl}/posts/${post.slug}`}
              title={post.title}
              separator=':: '
              aria-label='Compartilhar no Whatsapp'
            >
              <WhatsappIcon size={32} round />
            </WhatsappShareButton>
          </div>
        </div>
      )}
    </div>
  )
}
