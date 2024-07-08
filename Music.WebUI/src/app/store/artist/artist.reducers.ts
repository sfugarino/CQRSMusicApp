import { createReducer, on } from '@ngrx/store';
import { Artist } from './artist.model';
import { ArtistsAPIActions, ArtistsPageActions } from './artist.actions';

export interface ArtistsState {
  loading: boolean;
  artist: Artist[];
}

const initalState: ArtistsState = {
  loading: false,
  artist: [],
};

export const artistsReducer = createReducer(
  initalState,

  on(ArtistsPageActions.loadArtists, (state) => ({
    ...state,
    loading: true,
  })),
  on(ArtistsAPIActions.artistsLoadedSuccess, (state, { artists }) => ({
    ...state,
    loading: false,
    artists,
  }))
);
