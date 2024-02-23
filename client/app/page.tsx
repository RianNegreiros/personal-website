'use client'

import Feed from './components/Feed'

export default function IndexPage() {
  return (
    <div className='mx-auto flex min-h-screen max-w-6xl flex-col divide-y divide-gray-200 px-4 dark:divide-gray-700 sm:px-6 lg:px-8'>
      <div className='pb-8 pt-6 md:space-y-5'></div>
      <div className='pt-8'>
        <Feed />
      </div>
    </div>
  )
}
