import Card from './Card'

export default function CardPlaceholder() {
  const cardCount = 6
  const cards = Array.from({ length: cardCount })

  return (
    <div className='mx-auto flex min-h-screen max-w-6xl flex-col divide-y divide-gray-200 px-4 dark:divide-gray-700 sm:px-6 lg:px-8'>
      <div className='space-y-2 pb-8 pt-6 md:space-y-5'></div>
      <div className='grid gap-y-8 pt-8 sm:grid-cols-2 sm:gap-6 md:gap-6 lg:grid-cols-3 lg:gap-10'>
        {cards.map((_, index) => (
          <Card key={index} />
        ))}
      </div>
    </div>
  )
}
