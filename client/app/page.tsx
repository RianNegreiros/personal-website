'use client'

import Feed from './components/Feed'

export default function IndexPage() {
  return (
    <div className='mx-auto flex min-h-screen max-w-6xl flex-col divide-y divide-gray-900 px-4 dark:divide-gray-900 sm:px-6 lg:px-8'>
      <Feed />
    </div>
  )
}
