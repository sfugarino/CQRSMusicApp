import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { Artist } from './artist.model';

export const ArtistsPageActions = createActionGroup({
  source: 'Artists Page',
  events: {
    'Load Artists': emptyProps(),
    'Add Artist': props<{ artist: Artist }>(),
    'Update Artist': props<{ artist: Artist }>(),
    'Delete Artist': props<{ id: string }>(),
  },
});

export const ArtistsAPIActions = createActionGroup({
  source: 'Artists API',
  events: {
    'Artists Loaded Success': props<{ artists: Artist[] }>(),
    'Artists Loaded Fail': props<{ message: string }>(),
    'Artist Added Success': props<{ artist: Artist }>(),
    'Artist Added Fail': props<{ message: string }>(),
    'Artist Updated Success': props<{ artist: Artist }>(),
    'Artist Updated Fail': props<{ message: string }>(),
    'Artist Deleted Success': props<{ id: string }>(),
    'Artist Deleted Fail': props<{ message: string }>(),
  },
});
