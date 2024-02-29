import { useState, useEffect } from 'react'
import { AxiosError } from 'axios'
import { getFeed } from '@/app/utils/api'
import { useLoading } from '../contexts/LoadingContext';

export const useFeed = () => {
  const { isLoading, setLoading } = useLoading();
  const [data, setData] = useState([])
  const [error, setError] = useState<AxiosError | null>(null)

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true)
      try {
        const feed = await getFeed()
        setData(feed.data)
        setLoading(false)
      } catch (error) {
        setError(error as AxiosError)
        setLoading(false)
      }
    }
    fetchData()
  }, [setLoading])

  return { isLoading, data, error }
}
