// Upload file to firebase here

import { getStorage, ref, uploadString, getDownloadURL } from 'firebase/storage';

const storage = getStorage();

async function uploadToCloud(fileDataUrl: string, filename: string): Promise<string> {
  const storageRef = ref(storage, 'images/' + filename);
  const snapshot = await uploadString(storageRef, fileDataUrl, 'data_url');
  console.log('Uploaded a data_url string!');
  const downloadUrl = await getDownloadURL(snapshot.ref);
  console.log('File available at', downloadUrl);
  return downloadUrl;
}

export { uploadToCloud };
