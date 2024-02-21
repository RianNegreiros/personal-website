'use client'

import Link from 'next/link'
import { usePathname } from 'next/navigation'
import React from 'react'

export default function FloatingButtons() {
  const pathname = usePathname()

  if (pathname.includes('/posts/') || pathname.includes('/admin/')) {
    return null
  }

  const API_URL = process.env.NEXT_PUBLIC_API_URL

  return (
    <div className='fixed bottom-10 right-10 z-50 flex flex-col gap-2'>
      <Link
        href={`${API_URL}/posts/rss`}
        target='_blank'
        className='flex h-10 w-10 items-center justify-center rounded-full dark:bg-gray-800'
      >
        <svg
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 24 24'
          fill='currentColor'
          className='h-6 w-6 text-dracula-pink-400 hover:text-dracula-pink-600'
        >
          <path
            fillRule='evenodd'
            d='M3.75 4.5a.75.75 0 0 1 .75-.75h.75c8.284 0 15 6.716 15 15v.75a.75.75 0 0 1-.75.75h-.75a.75.75 0 0 1-.75-.75v-.75C18 11.708 12.292 6 5.25 6H4.5a.75.75 0 0 1-.75-.75V4.5Zm0 6.75a.75.75 0 0 1 .75-.75h.75a8.25 8.25 0 0 1 8.25 8.25v.75a.75.75 0 0 1-.75.75H12a.75.75 0 0 1-.75-.75v-.75a6 6 0 0 0-6-6H4.5a.75.75 0 0 1-.75-.75v-.75Zm0 7.5a1.5 1.5 0 1 1 3 0 1.5 1.5 0 0 1-3 0Z'
            clipRule='evenodd'
          />
        </svg>
      </Link>
      <Link
        href='https://cv.riannegreiros.dev'
        target='_blank'
        className='flex h-10 w-10 items-center justify-center rounded-full dark:bg-gray-800'
      >
        <svg
          xmlns='http://www.w3.org/2000/svg'
          fill='currentColor'
          className='h-8 w-8 text-dracula-pink-400 hover:text-dracula-pink-600'
          viewBox='0 0 1024 1024'
        >
          <path d='M880 112H144c-17.7 0-32 14.3-32 32v736c0 17.7 14.3 32 32 32h736c17.7 0 32-14.3 32-32V144c0-17.7-14.3-32-32-32zM380 696c-22.1 0-40-17.9-40-40s17.9-40 40-40 40 17.9 40 40-17.9 40-40 40zm0-144c-22.1 0-40-17.9-40-40s17.9-40 40-40 40 17.9 40 40-17.9 40-40 40zm0-144c-22.1 0-40-17.9-40-40s17.9-40 40-40 40 17.9 40 40-17.9 40-40 40zm304 272c0 4.4-3.6 8-8 8H492c-4.4 0-8-3.6-8-8v-48c0-4.4 3.6-8 8-8h184c4.4 0 8 3.6 8 8v48zm0-144c0 4.4-3.6 8-8 8H492c-4.4 0-8-3.6-8-8v-48c0-4.4 3.6-8 8-8h184c4.4 0 8 3.6 8 8v48zm0-144c0 4.4-3.6 8-8 8H492c-4.4 0-8-3.6-8-8v-48c0-4.4 3.6-8 8-8h184c4.4 0 8 3.6 8 8v48z' />
        </svg>
      </Link>
      <Link
        href='https://github.com/RianNegreiros'
        target='_blank'
        className='flex h-10 w-10 items-center justify-center rounded-full dark:bg-gray-800'
      >
        <svg
          viewBox='0 0 1024 1024'
          fill='currentColor'
          className='h-8 w-8 text-dracula-pink-400 hover:text-dracula-pink-600'
        >
          <path d='M511.6 76.3C264.3 76.2 64 276.4 64 523.5 64 718.9 189.3 885 363.8 946c23.5 5.9 19.9-10.8 19.9-22.2v-77.5c-135.7 15.9-141.2-73.9-150.3-88.9C215 726 171.5 718 184.5 703c30.9-15.9 62.4 4 98.9 57.9 26.4 39.1 77.9 32.5 104 26 5.7-23.5 17.9-44.5 34.7-60.8-140.6-25.2-199.2-111-199.2-213 0-49.5 16.3-95 48.3-131.7-20.4-60.5 1.9-112.3 4.9-120 58.1-5.2 118.5 41.6 123.2 45.3 33-8.9 70.7-13.6 112.9-13.6 42.4 0 80.2 4.9 113.5 13.9 11.3-8.6 67.3-48.8 121.3-43.9 2.9 7.7 24.7 58.3 5.5 118 32.4 36.8 48.9 82.7 48.9 132.3 0 102.2-59 188.1-200 212.9a127.5 127.5 0 0138.1 91v112.5c.8 9 0 17.9 15 17.9 177.1-59.7 304.6-227 304.6-424.1 0-247.2-200.4-447.3-447.5-447.3z' />
        </svg>
      </Link>
      <Link
        href='https://www.linkedin.com/in/riannegreiros'
        target='_blank'
        className='flex h-10 w-10 items-center justify-center rounded-full dark:bg-gray-800'
      >
        <svg
          viewBox='0 0 1024 1024'
          fill='currentColor'
          className='h-8 w-8 text-dracula-pink-400 hover:text-dracula-pink-600'
        >
          <path d='M880 112H144c-17.7 0-32 14.3-32 32v736c0 17.7 14.3 32 32 32h736c17.7 0 32-14.3 32-32V144c0-17.7-14.3-32-32-32zM349.3 793.7H230.6V411.9h118.7v381.8zm-59.3-434a68.8 68.8 0 1168.8-68.8c-.1 38-30.9 68.8-68.8 68.8zm503.7 434H675.1V608c0-44.3-.8-101.2-61.7-101.2-61.7 0-71.2 48.2-71.2 98v188.9H423.7V411.9h113.8v52.2h1.6c15.8-30 54.5-61.7 112.3-61.7 120.2 0 142.3 79.1 142.3 181.9v209.4z' />
        </svg>
      </Link>

      <Link
        href='mailto: riannegreiros@gmail.com'
        target='_blank'
        className='flex h-10 w-10 items-center justify-center rounded-full dark:bg-gray-800'
      >
        <svg
          viewBox='0 0 1024 1024'
          fill='currentColor'
          className='h-8 w-8 text-dracula-pink-400 hover:text-dracula-pink-600'
        >
          <path d='M928 160H96c-17.7 0-32 14.3-32 32v640c0 17.7 14.3 32 32 32h832c17.7 0 32-14.3 32-32V192c0-17.7-14.3-32-32-32zm-80.8 108.9L531.7 514.4c-7.8 6.1-18.7 6.1-26.5 0L189.6 268.9A7.2 7.2 0 01194 256h648.8a7.2 7.2 0 014.4 12.9z' />
        </svg>
      </Link>
    </div>
  )
}
